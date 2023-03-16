using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Constants;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Helpers;
using Mango.Services.ShoppingCartAPI.Repository;
using Mango.Services.ShoppingCartAPI.Senders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SD.CheckoutTopic = builder.Configuration["MessageBusTopics:CheckoutTopic"];

// Azure Service Bus
SD.ServiceBusConnectionString = builder.Configuration["ConnectionStrings:ServiceBus"];

// TODO: Uncomment if desired to utilize RabbitMQ
// RabbitMQ
// SD.RabbitMqHostname = builder.Configuration["RabbitMQ:Hostname"];
// SD.RabbitMqUsername = builder.Configuration["RabbitMQ:Username"];
// SD.RabbitMqPassword = builder.Configuration["RabbitMQ:Password"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

// Azure Service Bus
builder.Services.AddSingleton<IMessageBus, MessageBus>();

// TODO: Uncomment if desired to utilize RabbitMQ
// RabbitMQ
// builder.Services.AddSingleton<IRabbitMqSender, RabbitMqSender>();

builder.Services.AddControllers();
builder.Services.AddHttpClient<ICouponRepository, CouponRepository>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]));
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:44363/";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "mango");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Mango.Services.ShoppingCartAPI"});
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer' [space] and your token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
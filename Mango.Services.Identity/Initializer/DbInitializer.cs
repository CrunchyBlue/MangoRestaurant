using System.Security.Claims;
using IdentityModel;
using Mango.Services.Identity.Constants;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Initializer;

/// <summary>
/// The db initializer.
/// </summary>
public class DbInitializer : IDbInitializer
{
    /// <summary>
    /// The db context.
    /// </summary>
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// The user manager.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// The role manager.
    /// </summary>
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbInitializer"/> class.
    /// </summary>
    /// <param name="dbContext">
    /// The db context.
    /// </param>
    /// <param name="userManager">
    /// The user manager.
    /// </param>
    /// <param name="roleManager">
    /// The role manager.
    /// </param>
    public DbInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <inheritdoc cref="IDbInitializer.Initialize"/>
    public async Task Initialize()
    {
        if (_roleManager.FindByNameAsync(SD.Admin).Result != null)
        {
            return;
        }

        await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
        await _roleManager.CreateAsync(new IdentityRole(SD.Customer));

        var adminUser = new ApplicationUser
        {
            UserName = "Admin@example.com",
            Email = "Admin@example.com",
            EmailConfirmed = true,
            PhoneNumber = "1234567890",
            FirstName = "John",
            LastName = "Doe",
        };
        
        var customerUser = new ApplicationUser
        {
            UserName = "Customer@example.com",
            Email = "Customer@example.com",
            EmailConfirmed = true,
            PhoneNumber = "1234567890",
            FirstName = "Jane",
            LastName = "Doe",
        };

        await _userManager.CreateAsync(adminUser, "Admin#123");
        await _userManager.AddToRoleAsync(adminUser, SD.Admin);
        await _userManager.AddClaimsAsync(adminUser, new[]
        {
            new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Admin),
        });
        
        await _userManager.CreateAsync(customerUser, "Customer#123");
        await _userManager.AddToRoleAsync(customerUser, SD.Customer);
        await _userManager.AddClaimsAsync(customerUser, new[]
        {
            new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {customerUser.LastName}"),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Customer),
        });
    }
}
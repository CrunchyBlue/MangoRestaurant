using Mango.Services.OrderProcessor.Messaging;

namespace Mango.Services.OrderProcessor.Extensions;

/// <summary>
/// The application builder extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Gets or sets the service bus consumer.
    /// </summary>
    private static IServiceBusConsumer ServiceBusConsumer { get; set; }

    /// <summary>
    /// The use service bus consumer.
    /// </summary>
    /// <param name="app">
    /// The app.
    /// </param>
    /// <returns>
    /// The <see cref="IApplicationBuilder"/>
    /// </returns>
    public static IApplicationBuilder UseServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IServiceBusConsumer>();
        var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        if (hostApplicationLife != null)
        {
            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopped.Register(OnStop);
        }

        return app;
    }

    /// <summary>
    /// The on start.
    /// </summary>
    private static void OnStart()
    {
        ServiceBusConsumer.Start();
    }
    
    /// <summary>
    /// The on stop.
    /// </summary>
    private static void OnStop()
    {
        ServiceBusConsumer.Stop();
    }
}
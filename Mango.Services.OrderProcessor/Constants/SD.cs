﻿namespace Mango.Services.OrderProcessor.Constants;

/// <summary>
/// The static details.
/// </summary>
public static class SD
{
    /// <summary>
    /// Gets or sets the checkout topic.
    /// </summary>
    public static string CheckoutTopic { get; set; }

    /// <summary>
    /// Gets or sets the checkout subscription.
    /// </summary>
    public static string CheckoutSubscription { get; set; }
    
    /// <summary>
    /// Gets or sets the payment topic.
    /// </summary>
    public static string PaymentTopic { get; set; }
    
    /// <summary>
    /// Gets or sets the update order topic.
    /// </summary>
    public static string UpdateOrderTopic { get; set; }

    /// <summary>
    /// Gets or sets the update order subscription.
    /// </summary>
    public static string UpdateOrderSubscription { get; set; }
    
    /// <summary>
    /// Gets or sets the service bus connection string.
    /// </summary>
    public static string ServiceBusConnectionString { get; set; }
    
    /// <summary>
    /// Gets or sets the rabbitmq hostname.
    /// </summary>
    public static string RabbitMqHostname { get; set; }
    
    /// <summary>
    /// Gets or sets the rabbitmq username.
    /// </summary>
    public static string RabbitMqUsername { get; set; }
    
    /// <summary>
    /// Gets or sets the rabbitmq password.
    /// </summary>
    public static string RabbitMqPassword { get; set; }
}
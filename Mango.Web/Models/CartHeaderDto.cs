﻿namespace Mango.Web.Models;

/// <summary>
/// The cart header dto model.
/// </summary>
public class CartHeaderDto
{
    /// <summary>
    /// Gets or sets the cart header id.
    /// </summary>
    public int CartHeaderId { get; set; }

    /// <summary>
    /// Gets or sets the user id.
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the coupon code.
    /// </summary>
    public string CouponCode { get; set; }

    /// <summary>
    /// Gets or sets the order total.
    /// </summary>
    public double OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the discount total.
    /// </summary>
    public double DiscountTotal { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the pickup date time.
    /// </summary>
    public DateTime? PickupDateTime { get; set; }

    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public string CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the cvv.
    /// </summary>
    public string CVV { get; set; }

    /// <summary>
    /// Gets or sets the expiry month year.
    /// </summary>
    public string ExpiryMonthYear { get; set; }
}
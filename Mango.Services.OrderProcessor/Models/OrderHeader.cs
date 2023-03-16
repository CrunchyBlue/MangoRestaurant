using System.ComponentModel.DataAnnotations;

namespace Mango.Services.OrderProcessor.Models;

public class OrderHeader
{
    /// <summary>
    /// Gets or sets the order header id.
    /// </summary>
    [Key]
    public int OrderHeaderId { get; set; }

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

    /// <summary>
    /// Gets or sets the cart total items.
    /// </summary>
    public int CartTotalItems { get; set; }

    /// <summary>
    /// Gets or sets the payment status.
    /// </summary>
    public bool PaymentStatus { get; set; }

    /// <summary>
    /// Gets or sets the order date time.
    /// </summary>
    public DateTime OrderDateTime { get; set; }

    /// <summary>
    /// Gets or sets the cart details.
    /// </summary>
    public List<OrderDetail> OrderDetails { get; set; }
}
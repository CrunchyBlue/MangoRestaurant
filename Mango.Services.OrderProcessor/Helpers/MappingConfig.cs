using AutoMapper;
using Mango.Services.OrderProcessor.Messages;
using Mango.Services.OrderProcessor.Models;

namespace Mango.Services.OrderProcessor.Helpers;

/// <summary>
/// The mapping config.
/// </summary>
public class MappingConfig
{
    /// <summary>
    /// The register maps.
    /// </summary>
    /// <returns>
    /// The <see cref="MapperConfiguration"/>
    /// </returns>
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CheckoutHeaderDto, OrderHeader>()
                .ForMember(d => d.CartTotalItems,
                    opt => opt.MapFrom(s => s.CartDetails.Aggregate(0, (current, next) => current + next.Count)))
                .ForMember(d => d.OrderHeaderId, opt => opt.Ignore())
                .ForMember(d => d.OrderDateTime, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.PaymentStatus, opt => opt.MapFrom(s => false))
                .ForMember(d => d.OrderDetails, opt => opt.MapFrom(s => s.CartDetails));

            config.CreateMap<CartDetailDto, OrderDetail>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Product.Price));
        });

        return mappingConfig;
    }
}
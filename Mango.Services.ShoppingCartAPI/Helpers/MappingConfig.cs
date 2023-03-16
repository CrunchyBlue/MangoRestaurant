using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Helpers;

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
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
            
            config.CreateMap<CartHeaderDto, CartHeader>();
            config.CreateMap<CartHeader, CartHeaderDto>();
            
            config.CreateMap<CartDetailDto, CartDetail>();
            config.CreateMap<CartDetail, CartDetailDto>();

            config.CreateMap<CartDto, Cart>();
            config.CreateMap<Cart, CartDto>();
        });

        return mappingConfig;
    }
}
using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI.Helpers;

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
        });

        return mappingConfig;
    }
}
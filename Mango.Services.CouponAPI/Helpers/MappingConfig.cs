using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dtos;

namespace Mango.Services.CouponAPI.Helpers;

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
            config.CreateMap<CouponDto, Coupon>();
            config.CreateMap<Coupon, CouponDto>();
        });

        return mappingConfig;
    }
}
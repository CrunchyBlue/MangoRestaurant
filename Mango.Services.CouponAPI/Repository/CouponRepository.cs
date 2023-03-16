using AutoMapper;
using Mango.Services.CouponAPI.DbContexts;
using Mango.Services.CouponAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Repository;

/// <summary>
/// The coupon repository.
/// </summary>
public class CouponRepository : ICouponRepository
{
    /// <summary>
    /// The db context.
    /// </summary>
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// The mapper.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CouponRepository"/> class.
    /// </summary>
    /// <param name="dbContext">
    /// The db context.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    public CouponRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    /// <inheritdoc cref="ICouponRepository.GetCouponByCode"/>
    public async Task<CouponDto> GetCouponByCode(string couponCode)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        return _mapper.Map<CouponDto>(coupon);
    }
}
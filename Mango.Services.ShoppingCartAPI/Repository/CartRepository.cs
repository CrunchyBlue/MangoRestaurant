using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository;

/// <summary>
/// The cart repository.
/// </summary>
public class CartRepository : ICartRepository
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
    /// Initializes a new instance of the <see cref="CartRepository"/> class.
    /// </summary>
    /// <param name="dbContext">
    /// The db context.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    public CartRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <inheritdoc cref="ICartRepository.GetCartByUserId"/>
    public async Task<CartDto> GetCartByUserId(string userId)
    {
        var cartHeaderFromDb = await _dbContext.CartHeaders.FirstOrDefaultAsync(ch => ch.UserId == userId);
        var cartDetailsFromDb = _dbContext.CartDetails.Where(cd => cd.CartHeaderId == cartHeaderFromDb.CartHeaderId)
            .Include(cd => cd.Product);

        var cart = new Cart()
        {
            CartHeader = cartHeaderFromDb,
            CartDetails = cartDetailsFromDb
        };

        return _mapper.Map<CartDto>(cart);
    }

    /// <inheritdoc cref="ICartRepository.CreateUpdateCart"/>
    public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
    {
        var cart = _mapper.Map<Cart>(cartDto);

        var productInDb =
            await _dbContext.Products.FirstOrDefaultAsync(p =>
                p.ProductId == cart.CartDetails.FirstOrDefault().ProductId);

        if (productInDb == null)
        {
            var product = cart.CartDetails.FirstOrDefault()?.Product;

            if (product != null)
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        var cartHeaderFromDb = await _dbContext.CartHeaders.AsNoTracking()
            .FirstOrDefaultAsync(ch => ch.UserId == cart.CartHeader.UserId);

        if (cartHeaderFromDb == null)
        {
            _dbContext.CartHeaders.Add(cart.CartHeader);
            await _dbContext.SaveChangesAsync();
            var cartDetail = cart.CartDetails.FirstOrDefault();

            if (cartDetail != null)
            {
                cartDetail.Product = null; // Product has already been added
                cartDetail.CartHeaderId = cart.CartHeader.CartHeaderId;
                _dbContext.CartDetails.Add(cartDetail);
            }

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            var cartDetailFromDb = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(cd =>
                cd.CartHeaderId == cartHeaderFromDb.CartHeaderId &&
                cd.ProductId == cart.CartDetails.FirstOrDefault().ProductId);

            var cartDetail = cart.CartDetails.FirstOrDefault();

            if (cartDetailFromDb == null)
            {
                if (cartDetail != null)
                {
                    cartDetail.Product = null; // Product has already been added
                    cartDetail.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    _dbContext.CartDetails.Add(cartDetail);
                }
            }
            else
            {
                if (cartDetail != null)
                {
                    cartDetail.Product = null; // Product has already been added
                    cartDetail.Count += cartDetailFromDb.Count;
                    cartDetail.CartDetailId = cartDetailFromDb.CartDetailId;
                    cartDetail.CartHeaderId = cartDetailFromDb.CartHeaderId;
                    _dbContext.CartDetails.Update(cartDetail);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<CartDto>(cart);
    }

    /// <inheritdoc cref="ICartRepository.RemoveFromCart"/>
    public async Task<bool> RemoveFromCart(int cartDetailId)
    {
        try
        {
            var cartDetail = await _dbContext.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetailId == cartDetailId);
            var cartItemCount = _dbContext.CartDetails.Count(cd => cd.CartHeaderId == cartDetail.CartHeaderId);

            if (cartDetail == null)
            {
                return false;
            }

            _dbContext.CartDetails.Remove(cartDetail);

            // When removing the last item in cart, we should remove the header as well.
            if (cartItemCount == 1)
            {
                var cartHeaderToRemove =
                    await _dbContext.CartHeaders.FirstOrDefaultAsync(ch =>
                        ch.CartHeaderId == cartDetail.CartHeaderId);
                if (cartHeaderToRemove != null)
                {
                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    /// <inheritdoc cref="ICartRepository.ClearCart"/>
    public async Task<bool> ClearCart(string userId)
    {
        try
        {
            var cartHeaderFromDb = await _dbContext.CartHeaders.FirstOrDefaultAsync(ch => ch.UserId == userId);

            if (cartHeaderFromDb == null)
            {
                return false;
            }

            _dbContext.CartDetails.RemoveRange(
                _dbContext.CartDetails.Where(cd => cd.CartHeaderId == cartHeaderFromDb.CartHeaderId));
            _dbContext.CartHeaders.Remove(cartHeaderFromDb);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    /// <inheritdoc cref="ICartRepository.ApplyCoupon"/>
    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        try
        {
            var cart = await _dbContext.CartHeaders.FirstOrDefaultAsync(ch => ch.UserId == userId);
            if (cart == null)
            {
                return false;
            }
        
            cart.CouponCode = couponCode;
            _dbContext.CartHeaders.Update(cart);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    /// <inheritdoc cref="ICartRepository.RemoveCoupon"/>
    public async Task<bool> RemoveCoupon(string userId)
    {
        try
        {
            var cart = await _dbContext.CartHeaders.FirstOrDefaultAsync(ch => ch.UserId == userId);
            if (cart == null)
            {
                return false;
            }
        
            cart.CouponCode = string.Empty;
            _dbContext.CartHeaders.Update(cart);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
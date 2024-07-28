using BLL.Dtos;

namespace BLL.Services;

public interface ICartItemService : IService<CartItemDto>
{
    Task<IQueryable<CartItemDto>> GetAllFromCart(int cartId);
}
using DAL.Entities;

namespace DAL.Repositories;

public interface ICartItemRepository : ICrudRepository<CartItemEntity>
{
    IQueryable<CartItemEntity> GetAllFromCart(int? cartId);
}
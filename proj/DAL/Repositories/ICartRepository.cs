using DAL.Entities;

namespace DAL.Repositories;

public interface ICartRepository : ICrudRepository<CartEntity>
{
    CartEntity EditDiscount(CartEntity cartEntity, double disc);
}
using DAL.Entities;

namespace DAL.Repositories;

public class CartRepository(ApplicationDbContext dbContext) : ICartRepository
{
    public int Create(CartEntity cartEntity)
    {
        var cart = dbContext.Carts.Add(cartEntity);
        dbContext.SaveChanges();
        return cart.Entity.Id;
    }

    public CartEntity Read(int? id)
    {
        var cart = dbContext.Carts.FirstOrDefault(ca => ca.Id == id);
        return cart ?? throw new ArgumentException(nameof(id));;
    }

    public void Update(CartEntity cartEntity)
    {
        dbContext.Update(cartEntity);
        dbContext.SaveChanges();
    }

    public void Delete(int? id)
    {
        var cart = dbContext.Carts.FirstOrDefault(ca => ca.Id == id);
        if (cart == null)
        {
            throw new ArgumentException(nameof(id));
        }
        dbContext.Remove(cart);
        dbContext.SaveChanges();
    }

    public CartEntity EditDiscount(CartEntity cartEntity, double disc)
    {
        if (disc < 0)
            throw new ArgumentException(nameof(disc));
        cartEntity.Discount = disc;
        dbContext.Update(cartEntity);
        return cartEntity;
    }
}
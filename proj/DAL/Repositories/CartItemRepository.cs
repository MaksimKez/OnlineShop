using DAL.Entities;

namespace DAL.Repositories;

public class CartItemRepository(ApplicationDbContext dbContext) : ICartItemRepository
{
    public int Create(CartItemEntity cartEntity)
    {
        var item = dbContext.CartItems.Add(cartEntity);
        dbContext.SaveChanges();
        return item.Entity.Id;
    }

    public CartItemEntity Read(int id)
    {
        var item = dbContext.CartItems.FirstOrDefault(it => it.Id == id);
        return item ?? throw new ArgumentException(nameof(id));
    }

    public void Update(CartItemEntity cartItemEntity)
    {
        dbContext.Update(cartItemEntity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var item = dbContext.CartItems.FirstOrDefault(it => it.Id == id);
        if(item == null)
            throw new ArgumentException(nameof(id));
        dbContext.CartItems.Remove(item);
        dbContext.SaveChanges();
    }

    public IQueryable<CartItemEntity> GetAllFromCart(int cartId)
    {
        return dbContext.CartItems.Where(it => it.CartId == cartId).AsQueryable();
    }
}
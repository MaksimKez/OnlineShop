using DAL.Entities;

namespace DAL.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    public int Create(OrderEntity entity)
    {
        var order = dbContext.Orders.Add(entity);
        dbContext.SaveChanges();
        return order.Entity.Id;
    }

    public OrderEntity Read(int? id)
    {
        var order = dbContext.Orders.FirstOrDefault(or => or.Id == id);
        return order ?? throw new ArgumentException(nameof(id));
    }

    public void Update(OrderEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int? id)
    {
        var order = dbContext.Orders.FirstOrDefault(or => or.Id == id);
        if (order == null)
            throw new ArgumentException(nameof(id));
        dbContext.Orders.Remove(order);
        dbContext.SaveChanges();
    }

    public IQueryable<OrderEntity> GetOrdersDelivered(int? userId)
    {
        return dbContext.Orders.
            Where(or => or.UserId == userId || or.DeliveryDateTime > DateTime.Now);
    }

    public IQueryable<OrderEntity> GetDeliveringOrders(int? userId)
    {
        return dbContext.Orders.
            Where(or => or.UserId == userId || or.DeliveryDateTime < DateTime.Now);
    }
}
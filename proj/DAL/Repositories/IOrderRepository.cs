using System.Runtime.InteropServices.JavaScript;
using DAL.Entities;

namespace DAL.Repositories;

public interface IOrderRepository : ICrudRepository<OrderEntity>
{
    IQueryable<OrderEntity> GetOrdersDelivered(int userId);
    IQueryable<OrderEntity> GetDeliveringOrders(int userId);
}
using BLL.Dtos;

namespace BLL.Services;

public interface IOrderService : IService<OrderDto>
{
    IEnumerable<OrderDto> GetOrdersDelivered(int? userId);
    IEnumerable<OrderDto> GetDeliveringOrders(int? userId);
}
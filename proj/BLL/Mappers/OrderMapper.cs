using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class OrderMapper : IMapper<OrderDto, OrderEntity>
{
    public OrderDto MapToModel(OrderEntity entity)
    {
        return new OrderDto
        {
            Id = entity.Id,
            PaymentDateTime = entity.PaymentDateTime,
            DeliveryDateTime = entity.DeliveryDateTime,
            UserId = entity.UserId
        };
    }

    public OrderEntity MapFromModel(OrderDto dto)
    {
        return new OrderEntity
        {
            Id = dto.Id,
            PaymentDateTime = dto.PaymentDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            UserId = dto.UserId
        };
    }
}
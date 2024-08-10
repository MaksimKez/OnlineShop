using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class OrderMapperVm : IMapperVMs<OrderViewModel, OrderDto>
{
    public OrderViewModel MapToVm(OrderDto dto)
    {
        return new OrderViewModel
        {
            Id = dto.Id,
            PaymentDateTime = dto.PaymentDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            UserId = dto.UserId
        };
    }

    public OrderDto? MapToDto(OrderViewModel? viewModel)
    {
        if (viewModel is null) return null;
        return new OrderDto
        {
            Id = viewModel.Id,
            PaymentDateTime = viewModel.PaymentDateTime,
            DeliveryDateTime = viewModel.DeliveryDateTime,
            UserId = viewModel.UserId
        };
    }
}
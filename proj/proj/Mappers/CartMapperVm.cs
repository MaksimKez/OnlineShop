using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class CartMapperVm : IMapperVMs<CartViewModel, CartDto>
{
    public CartViewModel MapToVm(CartDto dto)
    {
        return new CartViewModel()
        {
            Id = dto.Id,
            Discount = dto.Discount,
            TotalPrice = dto.TotalPrice
        };
    }
    

    public CartDto MapToDto(CartViewModel viewModel)
    {
        return new CartDto()
        {
            Id = viewModel.Id,
            Discount = viewModel.Discount,
            TotalPrice = viewModel.TotalPrice
        };
    }
}
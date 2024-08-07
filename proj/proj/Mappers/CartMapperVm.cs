using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class CartMapperVm : IMapperVMs<CartViewModel, CartDto>
{
    public CartViewModel? MapToVm(CartDto? dto)
    {
        if (dto is null) return null;
        return new CartViewModel()
        {
            Id = dto.Id,
            Discount = dto.Discount,
            TotalPrice = dto.TotalPrice
        };
    }
    

    public CartDto? MapToDto(CartViewModel? viewModel)
    {
        if (viewModel is null) return null;
        return new CartDto()
        {
            Id = viewModel.Id,
            Discount = viewModel.Discount,
            TotalPrice = viewModel.TotalPrice
        };
    }
}
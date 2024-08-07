using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class CartItemMapperVm : IMapperVMs<CartItemViewModel, CartItemDto>
{
    public CartItemViewModel? MapToVm(CartItemDto? dto)
    {
        if (dto is null) return null;
        return new CartItemViewModel
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }

    public CartItemDto? MapToDto(CartItemViewModel? viewModel)
    {
        if (viewModel is null) return null;
        return new CartItemDto()
        {
            Id = viewModel.Id,
            CartId = viewModel.CartId,
            ProductId = viewModel.ProductId,
            Quantity = viewModel.Quantity
        };
    }
}
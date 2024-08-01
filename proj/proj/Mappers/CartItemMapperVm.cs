using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class CartItemMapperVm : IMapperVMs<CartItemViewModel, CartItemDto>
{
    public CartItemViewModel MapToVm(CartItemDto dto)
    {
        return new CartItemViewModel
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }

    public CartItemDto MapToDto(CartItemViewModel viewModel)
    {
        return new CartItemDto()
        {
            Id = viewModel.Id,
            CartId = viewModel.CartId,
            ProductId = viewModel.ProductId,
            Quantity = viewModel.Quantity
        };
    }
}
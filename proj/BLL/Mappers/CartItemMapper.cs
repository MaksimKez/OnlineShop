using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class CartItemMapper : IMapper<CartItemDto, CartItemEntity>
{
    public CartItemDto? MapToModel(CartItemEntity? entity)
    {
        if (entity is null) return null;
        return new CartItemDto()
        {
            Id = entity.Id,
            CartId = entity.CartId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity
        };
    }

    public CartItemEntity? MapFromModel(CartItemDto? dto)
    {
        if (dto is null) return null;
        return new CartItemEntity()
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }
}
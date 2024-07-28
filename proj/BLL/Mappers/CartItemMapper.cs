using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class CartItemMapper : IMapper<CartItemDto, CartItemEntity>
{
    public CartItemDto MapToModel(CartItemEntity entity)
    {
        return new CartItemDto()
        {
            Id = entity.Id,
            CartId = entity.CartId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity
        };
    }

    public CartItemEntity MapFromModel(CartItemDto dto)
    {
        return new CartItemEntity()
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }
}
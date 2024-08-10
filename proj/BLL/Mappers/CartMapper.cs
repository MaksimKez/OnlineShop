using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class CartMapper : IMapper<CartDto, CartEntity>
{
    public CartDto? MapToModel(CartEntity? cartEntity)
    {
        if (cartEntity is null) return null;
        return new CartDto()
        {
            Id = cartEntity.Id,
            Discount = cartEntity.Discount,
            TotalPrice = cartEntity.TotalPrice
        };
    }
    public CartEntity? MapFromModel(CartDto? cartDto)
    {
        if (cartDto is null) return null;
        return new CartEntity()
        {
            Id = cartDto.Id,
            Discount = cartDto.Discount,
            TotalPrice = cartDto.TotalPrice
        };
    }
}
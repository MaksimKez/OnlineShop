using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class CartMapper : IMapper<CartDto, CartEntity>
{
    public CartDto MapToModel(CartEntity cartEntity)
    {
        return new CartDto()
        {
            Id = cartEntity.Id,
            Discount = cartEntity.Discount,
            TotalPrice = cartEntity.TotalPrice
        };
    }
    public CartEntity MapFromModel(CartDto cartDto)
    {
        return new CartEntity()
        {
            Id = cartDto.Id,
            Discount = cartDto.Discount,
            TotalPrice = cartDto.TotalPrice
        };
    }
}
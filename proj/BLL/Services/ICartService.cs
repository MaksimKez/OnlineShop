using BLL.Dtos;
using DAL.Entities;

namespace BLL.Services;

public interface ICartService : IService<CartDto>
{
    CartDto EditDiscount(CartDto dto, double discount);
}
using BLL.Dtos;
using DAL.Entities;

namespace BLL.Services;

public interface IUserService : IService<UserDto>
{ 
    Task<IEnumerable<UserDto>> GetAllWithoutOrders();
}
using DAL;
using DAL.Entities;
using proj.Models;
using proj.Models.DTO;

namespace proj.Mappers;

public interface IUserMapper
{
    UserEntity MapToEntity(User user);
    User MapToUser(RegistrationRequestDto requestDto, ApplicationDbContext context);
    User MapToUser(UserEntity entity);

}
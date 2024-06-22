using DAL;
using DAL.Entities;
using proj.Models;
using proj.Models.DTO;

namespace proj.Mappers;

public class UserMapper : IUserMapper
{
    public UserEntity MapToEntity(User user)
    {
        if (user is null) return null;
        return new UserEntity()
        {
            CartId = user.CartId,
            Username = user.Username,
            Password = user.Password,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };
    }

    public User MapToUser(RegistrationRequestDto requestDto, ApplicationDbContext context)
    {
        if (requestDto is null) return null;
        var user = new User()
        {
            Username = requestDto.Username,
            FirstName = requestDto.FirstName,
            SecondName = requestDto.SecondName,
            Password = requestDto.Password,
            Email = requestDto.Email,
            PhoneNumber = requestDto.PhoneNumber,
            Role = requestDto.Role
        };
        if (requestDto.Role == "admin")
            user.CartId = null;
        else user.CartId = context.Users.Last().CartId + 1;
        return user;
    }

    public User MapToUser(UserEntity entity)
    {
        if (entity is null) return null;
        var user = new User()
        {
            Username = entity.Username,
            FirstName = entity.FirstName,
            SecondName = entity.SecondName,
            Password = entity.Password,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Role = entity.Role,
            CartId = entity.CartId
        };
        return user;
    }
}
using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class UserMapper : IMapper<UserDto, UserEntity>
{
    public UserDto? MapToModel(UserEntity? userEntity)
    {
        if (userEntity is null) return null;
        return new UserDto
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            FirstName = userEntity.FirstName,
            SecondName = userEntity.SecondName,
            DateOfBirth = userEntity.DateOfBirth,
            Email = userEntity.Email,
            PhoneNumber = userEntity.PhoneNumber,
            CartId = userEntity.CartId
        };
    }

    public UserEntity? MapFromModel(UserDto? userDto)
    {
        if (userDto is null) return null;
        return new UserEntity
        {
            Id = userDto.Id,
            Username = userDto.Username,
            FirstName = userDto.FirstName,
            SecondName = userDto.SecondName,
            DateOfBirth = userDto.DateOfBirth,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            CartId = userDto.CartId,
        };
    }
}
using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class UserMapperVm : IMapperVMs<UserViewModel, UserDto>
{
    public UserViewModel MapToVm(UserDto dto)
    {
        return new UserViewModel
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            CartId = dto.CartId
        };
    }

    public UserDto MapToDto(UserViewModel viewModel)
    {
        return new UserDto
        {
            Id = viewModel.Id,
            Username = viewModel.Username,
            FirstName = viewModel.FirstName,
            SecondName = viewModel.SecondName,
            DateOfBirth = viewModel.DateOfBirth,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            CartId = viewModel.CartId
        };
    }
}
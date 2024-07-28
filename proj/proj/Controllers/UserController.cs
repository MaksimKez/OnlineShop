using System.Data.SqlTypes;
using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetById/{id:int}")]
    public UserViewModel Get(int id)
    {
        var dto = _userService.Get(id);
        return new UserViewModel()
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            CartId = dto.CartId,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
    }

    [HttpGet("GetWithoutOrders")]
    public async Task<UserViewModel[]> GetWithoutOrders()
    {
        var dtos = await _userService.GetAllWithoutOrders();
        var users = dtos.Select(dto => new UserViewModel()
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            CartId = dto.CartId,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        });
        return users.ToArray();
    }

    [HttpPost("CreateUser")]
    public UserViewModel Create([FromBody] UserViewModel userViewModel)
    {
        var dto = new UserDto()
        {
            Id = userViewModel.Id,
            Username = userViewModel.Username,
            FirstName = userViewModel.FirstName,
            SecondName = userViewModel.SecondName,
            CartId = userViewModel.CartId,
            DateOfBirth = userViewModel.DateOfBirth,
            Email = userViewModel.Email,
            PhoneNumber = userViewModel.PhoneNumber
        };
        userViewModel.Id = _userService.Create(dto);
        return userViewModel;
    }

    [HttpPut("UpdateUser")]
    public UserViewModel UpdateUser([FromBody] UserViewModel userViewModel)
    {
        _userService.Update(new UserDto()
        {
            Id = userViewModel.Id,
            Username = userViewModel.Username,
            FirstName = userViewModel.FirstName,
            SecondName = userViewModel.SecondName,
            CartId = userViewModel.CartId,
            DateOfBirth = userViewModel.DateOfBirth,
            Email = userViewModel.Email,
            PhoneNumber = userViewModel.PhoneNumber
        });
        return userViewModel;
    }

    [HttpDelete("Delete/{id:int}")]
    public bool Delete(int id)
    {
        try
        {
            _userService.Delete(id);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
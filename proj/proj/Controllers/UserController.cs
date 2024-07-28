using System.Data.SqlTypes;
using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetById/{id:int}")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserViewModel> Get(int id)
    {
        try
        {
            var dto = _userService.Get(id);

            var userViewModel = new UserViewModel()
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
            return Ok(userViewModel);
        }
        catch (ArgumentException e)
        {
            return NotFound();
        }
    }

    [HttpGet("GetWithoutOrders")]
    [ProducesResponseType(typeof(UserViewModel[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserViewModel[]>> GetWithoutOrders()
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
        }).ToArray();
        return Ok(users);
    }

    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserViewModel> Create([FromBody] UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
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
        return CreatedAtAction(nameof(Get), new { id = userViewModel.Id }, userViewModel);
    }

    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserViewModel> UpdateUser([FromBody] UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
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
            return Ok(userViewModel);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("Delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        try
        {
            _userService.Delete(id);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}

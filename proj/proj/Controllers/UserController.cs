using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.Mappers;
using proj.ViewModels;

namespace proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapperVMs<UserViewModel, UserDto> _mapper;

    public UserController(IUserService userService, IMapperVMs<UserViewModel, UserDto> mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("GetUserById/{id:int}")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserViewModel> Get([FromQuery]int? id)
    {
        if (id is null or < 0) return NotFound();
        var dto = _userService.Get(id);

        return Ok(_mapper.MapToVm(dto));
    }

    [HttpGet("GetWithoutOrders")]
    [ProducesResponseType(typeof(UserViewModel[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserViewModel[]>> GetWithoutOrders()
    {
        var dtos = await _userService.GetAllWithoutOrders();
        var users = dtos.Select(dto => _mapper.MapToVm(dto)).ToArray();
        return Ok(users);
    }

    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserViewModel> Create(UserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        userViewModel.Id = _userService.Create(_mapper.MapToDto(userViewModel));
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
            _userService.Update(_mapper.MapToDto(userViewModel));
            return Ok(userViewModel);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("DeleteUser/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int? id)
    {
        if(id is null or < 0) return NotFound();
        _userService.Delete(id);
        return Ok();
    }
}

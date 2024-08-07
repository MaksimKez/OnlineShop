using BLL.Dtos;
using BLL.FluentValidation;
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
    private readonly UserValidator _userValidator;
    public UserController(IUserService userService, IMapperVMs<UserViewModel, UserDto> mapper, UserValidator userValidator)
    {
        _userService = userService;
        _mapper = mapper;
        _userValidator = userValidator;
    }

    [HttpGet("GetUserById")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserViewModel> Get([FromQuery]int? id)
    {
        if (id is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dto = _userService.Get(id);
        if (dto is null) return NotFound();
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
    public ActionResult<UserViewModel> Create([FromBody] UserViewModel userViewModel)
    {
        var dto = _mapper.MapToDto(userViewModel);
        var validationResult = _userValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        userViewModel.Id = _userService.Create(dto!); 
        return CreatedAtAction(nameof(Create), new { id = userViewModel.Id }, userViewModel);
    }

    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)] [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserViewModel> UpdateUser([FromBody] UserViewModel userViewModel)
    {
        var dto = _mapper.MapToDto(userViewModel);
        var validationResult = _userValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _userService.Update(_mapper.MapToDto(userViewModel));
        return Ok(userViewModel);
    }

    [HttpDelete("DeleteUser/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute]int? id)
    { 
        if (id is null or < 0) return BadRequest();
        try 
        { 
            _userService.Delete(id);
            return NoContent();
        }
        catch (ArgumentException e) 
        { 
            return NotFound();
        }
    }
}

using BLL.Dtos;
using BLL.FluentValidation;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using proj.Mappers;
using proj.ViewModels;

namespace proj.Controllers;

public class CartItemController : ControllerBase
{
    private readonly ICartItemService _service;
    private readonly IMapperVMs<CartItemViewModel, CartItemDto> _mapper;
    private readonly CartItemValidator _validator;

    public CartItemController(ICartItemService service, CartItemValidator validator, IMapperVMs<CartItemViewModel, CartItemDto> mapper)
    {
        _service = service ?? throw new ArgumentException("Service err", nameof(service));
        _validator = validator ?? throw new ArgumentException("Service err", nameof(validator));;
        _mapper = mapper ?? throw new ArgumentException("Service err", nameof(mapper));
    }

    [HttpGet("GetCartItemId")]
    [ProducesResponseType(typeof(CartItemViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CartItemViewModel> GetById([FromBody] int? id)
    {
        if (id is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dto = _service.Get(id);
        if (dto is null) return NotFound();
        return Ok(_mapper.MapToVm(dto));
    }

    [HttpGet("GetItemByCartId")]
    [ProducesResponseType(typeof(List<CartItemViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CartItemViewModel>>> GetAllFromCart([FromQuery]int? cartId)
    {  
        if (cartId is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dtos = await _service.GetAllFromCart(cartId);
        if (dtos.IsNullOrEmpty()) return NotFound();
        return Ok(dtos.Select(dto => _mapper.MapToVm(dto)));
    }

    [HttpPost("CreateItem")]
    [ProducesResponseType(typeof(CartItemViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CartItemViewModel> Create([FromBody] CartItemViewModel cartItemViewModel)
    {   
        var dto = _mapper.MapToDto(cartItemViewModel);
        var validationResult = _validator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        cartItemViewModel.Id = _service.Create(dto!); 
        return CreatedAtAction(nameof(Create), new { id = cartItemViewModel.Id }, cartItemViewModel);
    }

    [HttpPut("UpdateItem")]  
    [ProducesResponseType(typeof(CartItemViewModel), StatusCodes.Status200OK)] [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CartItemViewModel> Update([FromBody] CartItemViewModel cartItemViewModel)
    {
        var dto = _mapper.MapToDto(cartItemViewModel);
        var validationResult = _validator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _service.Update(dto!);
        return Ok(cartItemViewModel);

    }

    [HttpDelete("DeleteItem{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int? id)
    {
        if (id is null or < 0) return BadRequest();
        try 
        { 
            _service.Delete(id);
            return NoContent();
        }
        catch (ArgumentException) 
        { 
            return NotFound();
        }
    }
}
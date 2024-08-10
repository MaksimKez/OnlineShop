using BLL.Dtos;
using BLL.FluentValidation;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.Mappers;
using proj.ViewModels;

namespace proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapperVMs<CartViewModel, CartDto> _mapper;
    private readonly CartValidator _validator;
 
    public CartController(ICartService cartService, CartValidator validator, IMapperVMs<CartViewModel, CartDto> mapper)
    {
        _cartService = cartService ?? throw new ArgumentException("Service err", nameof(cartService));
        _validator = validator ?? throw new ArgumentException("Validator err", nameof(validator));
        _mapper = mapper ?? throw new ArgumentException("Mapper err", nameof(mapper));
    }

    [HttpPost("CreateCart")]
    [ProducesResponseType(typeof(CartViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CartViewModel> Create([FromBody] CartViewModel cartViewModel)
    {
        var dto = _mapper.MapToDto(cartViewModel);
        var validationResult = _validator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        cartViewModel.Id = _cartService.Create(dto!); 
        return CreatedAtAction(nameof(Create), new { id = cartViewModel.Id }, cartViewModel);
    }

    [HttpGet("GetCartById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CartViewModel> GetById([FromBody] int? id)
    {
        if (id is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dto = _cartService.Get(id);
        if (dto is null) return NotFound();
        return Ok(_mapper.MapToVm(dto));
    }

    [HttpPut("UpdateCart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CartViewModel> Update([FromBody] CartViewModel cartViewModel)
    {
        var dto = _mapper.MapToDto(cartViewModel);
        var validationResult = _validator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _cartService.Update(dto!);
        return Ok(cartViewModel);
    }
    
    [HttpPatch("EditDiscount{newDiscount:double}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CartViewModel> EditDiscount(double newDiscount, [FromBody] CartViewModel cartViewModel)
    {
        var dtoBefore = _mapper.MapToDto(cartViewModel);
        var validationResult = _validator.Validate(dtoBefore!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        var dtoAfter = _cartService.EditDiscount(dtoBefore!, newDiscount);
        return Ok(_mapper.MapToVm(dtoAfter)!);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("DeleteCart{id:int}")]
    public ActionResult Delete(int? id)
    {
        if (id is null or < 0) return BadRequest();
        try
        {
            _cartService.Delete(id);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}
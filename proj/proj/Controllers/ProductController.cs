using BLL.Dtos;
using BLL.FluentValidation;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using proj.Mappers;
using proj.ViewModels;

namespace proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapperVMs<ProductViewModel, ProductDto> _mapper;
    private readonly ProductValidator _productValidator;

    public ProductController(IProductService service, IMapperVMs<ProductViewModel, ProductDto> mapper, ProductValidator productValidator)
    {
        _service = service ?? throw new ArgumentException("Service err", nameof(service));
        _mapper = mapper ?? throw new ArgumentException("Mapper err", nameof(service));
        _productValidator = productValidator ?? throw new ArgumentException("Validator err", nameof(service));
    }

    [HttpGet("GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductViewModel> GetById([FromBody] int? id)
    {
        if (id is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dto = _service.Get(id);
        if (dto is null) return NotFound();
        return Ok(_mapper.MapToVm(dto));
    }

    [HttpGet("GetOutOfStockProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductViewModel>>> GetOutOfStock()
    {
        var dtos = await _service.GetOutOfStock();
        if (dtos.IsNullOrEmpty()) return NotFound();
        var products = dtos.Select(dto => _mapper.MapToVm(dto));
        return products.ToList()!;
    }

    [HttpPost("CreateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductViewModel> Create([FromBody] ProductViewModel productViewModel)
    {
        var dto = _mapper.MapToDto(productViewModel);
        productViewModel.Id = _service.Create(dto!);
        return Ok(productViewModel);
    }

    [HttpPut("UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductViewModel> Update([FromBody] ProductViewModel productViewModel)
    {
        var dto = _mapper.MapToDto(productViewModel);
        var validationResult = _productValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _service.Update(dto!);
        return Ok(productViewModel);
    }

    [HttpDelete("DeleteProduct/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public bool Delete(int id)
    {
        try
        {
            _service.Delete(id);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    [HttpPatch("IncrementStock/{count:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult IncrementStock([FromBody] ProductViewModel productViewModel, [FromQuery] int count)
    {
        var dto = _mapper.MapToDto(productViewModel);
        var validationResult = _productValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _service.IncrementStock(dto!, count);
        return Ok();
    }
    
    [HttpPatch("DecrementStock/{count:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult DecrementStock([FromBody] ProductViewModel productViewModel, [FromQuery] int count)
    {
        var dto = _mapper.MapToDto(productViewModel);
        var validationResult = _productValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _service.DecrementStock(dto!, count);
        return Ok();
    }
}

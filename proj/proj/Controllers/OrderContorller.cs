using BLL.Dtos;
using BLL.FluentValidation;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using proj.Mappers;
using proj.ViewModels;

namespace proj.Controllers;

public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapperVMs<OrderViewModel, OrderDto> _mapper;
    private readonly OrderValidator _orderValidator;

    public OrderController(IOrderService service, OrderValidator orderValidator, IMapperVMs<OrderViewModel, OrderDto> mapper)
    {
        _service = service ?? throw new ArgumentException("Service err", nameof(service));
        _orderValidator = orderValidator ?? throw new ArgumentException("Service err", nameof(orderValidator));;
        _mapper = mapper ?? throw new ArgumentException("Service err", nameof(mapper));
    }

    [HttpGet("GetOrderById")]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderViewModel> GetById([FromQuery]int? id)
    {
        if (id is null or < 0) return BadRequest("Id must be greater than zero and must not be null.");
        var dto = _service.Get(id);
        if (dto is null) return NotFound();
        return Ok(_mapper.MapToVm(dto));
    }

    [HttpPost("CreateOrder")]
    [ProducesResponseType(typeof(OrderViewModel),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderViewModel> Create([FromBody] OrderViewModel orderViewModel)
    {
        var dto = _mapper.MapToDto(orderViewModel);
        var validationResult = _orderValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        orderViewModel.Id = _service.Create(dto!); 
        return CreatedAtAction(nameof(Create), new { id = orderViewModel.Id }, orderViewModel);
    }

    [HttpPut("UpdateOrder")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderViewModel> Update([FromBody] OrderViewModel orderViewModel)
    {
        var dto = _mapper.MapToDto(orderViewModel);
        var validationResult = _orderValidator.Validate(dto!);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        _service.Update(dto!);
        return Ok(orderViewModel);
    }

    [HttpDelete("DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete([FromQuery]int? id)
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

    [HttpGet("GetOrdersDelivered")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderViewModel[]> GetOrdersDelivered([FromQuery] int? userId)
    {
        if (userId is null or < 0) return BadRequest();
        var dtos = _service.GetOrdersDelivered(userId);
        if (dtos.IsNullOrEmpty()) return NotFound();

        return Ok(dtos.Select(dto => _mapper.MapToVm(dto)!).ToArray());
    }

    [HttpGet("GetDeliveringOrders")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderViewModel[]> GetDeliveringOrders([FromQuery] int? userId)
    {
        if (userId is null or < 0) return BadRequest();
        var dtos = _service.GetDeliveringOrders(userId);
        if (dtos.IsNullOrEmpty()) return NotFound();

        return Ok(dtos.Select(dto => _mapper.MapToVm(dto)!).ToArray());
    }
}

using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service ?? throw new ArgumentException(nameof(service), "Service err");
    }

    [HttpGet("GetOrderById{id:int}")]
    public OrderViewModel GetById(int id)
    {
        var dto = _service.Get(id);
        return new OrderViewModel
        {
            Id = dto.Id,
            PaymentDateTime = dto.PaymentDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            UserId = dto.UserId
        };
    }

    [HttpPost("CreateOrder")]
    public OrderViewModel Create([FromBody] OrderViewModel orderViewModel)
    {
        var dto = new OrderDto()
        {
            Id = orderViewModel.Id,
            PaymentDateTime = orderViewModel.PaymentDateTime,
            DeliveryDateTime = orderViewModel.DeliveryDateTime,
            UserId = orderViewModel.UserId
        };
        orderViewModel.Id = _service.Create(dto);
        return orderViewModel;
    }

    [HttpPut("UpdateOrder")]
    public OrderViewModel Update([FromBody] OrderViewModel orderViewModel)
    {
        _service.Update(new OrderDto()
        {
            Id = orderViewModel.Id,
            PaymentDateTime = orderViewModel.PaymentDateTime,
            DeliveryDateTime = orderViewModel.DeliveryDateTime,
            UserId = orderViewModel.UserId
        });
        return orderViewModel;
    }

    [HttpDelete("DeleteOrder/{id:int}")]
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

    [HttpGet("GetOrdersDelivered/{userId:int}")]
    public IEnumerable<OrderViewModel> GetOrdersDelivered(int userId)
    {
        var dtos = _service.GetOrdersDelivered(userId);
        return dtos.Select(dto => new OrderViewModel
        {
            Id = dto.Id,
            PaymentDateTime = dto.PaymentDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            UserId = dto.UserId
        });
    }

    [HttpGet("GetDeliveringOrders/{userId:int}")]
    public IEnumerable<OrderViewModel> GetDeliveringOrders(int userId)
    {
        var dtos = _service.GetDeliveringOrders(userId);
        return dtos.Select(dto => new OrderViewModel
        {
            Id = dto.Id,
            PaymentDateTime = dto.PaymentDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            UserId = dto.UserId
        });
    }
}

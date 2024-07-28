using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("CreateCart")]
    public CartViewModel Create([FromBody] CartViewModel cartViewModel)
    {
        var dto = new CartDto
        {
            Id = cartViewModel.Id,
            Discount = cartViewModel.Discount,
            TotalPrice = cartViewModel.TotalPrice
        };
        cartViewModel.Id = _cartService.Create(dto);
        return cartViewModel;
    }

    [HttpGet("GetCartById")]
    public CartViewModel GetById([FromBody] int id)
    {
        var dto = _cartService.Get(id);
        return new CartViewModel()
        {
            Id = dto.Id,
            Discount = dto.Discount,
            TotalPrice = dto.TotalPrice
        };
    }

    [HttpPut("UpdateCart")]
    public CartViewModel Update([FromBody] CartViewModel cartViewModel)
    {
        var dto = new CartDto()
        {
            Id = cartViewModel.Id,
            Discount = cartViewModel.Discount,
            TotalPrice = cartViewModel.TotalPrice
        };
        _cartService.Update(dto);
        return cartViewModel;
    }

    [HttpPatch("EditDiscount{newDiscount:double}")]
    public CartViewModel EditDiscount(double newDiscount, [FromBody] CartViewModel cartViewModel)
    {
        var dtoBefore = new CartDto()
        {
            Id = cartViewModel.Id,
            Discount = cartViewModel.Discount,
            TotalPrice = cartViewModel.TotalPrice
        };
        var dtoAfter = _cartService.EditDiscount(dtoBefore, newDiscount);
        return new CartViewModel()
        {
            Id = dtoAfter.Id,
            Discount = dtoAfter.Discount,
            TotalPrice = dtoAfter.TotalPrice
        };
    }

    [HttpDelete("DeleteCart{id:int}")]
    public bool Delete(int id)
    {
        try
        {
            _cartService.Delete(id);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
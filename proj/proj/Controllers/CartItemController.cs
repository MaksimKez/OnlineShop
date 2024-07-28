using BLL.Dtos;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet("GetCartItemId")]
    public CartItemViewModel GetById([FromBody] int id)
    {
        var dto = _cartItemService.Get(id);
        return new CartItemViewModel
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }

    [HttpGet("GetItemById{cartId:int}")]
    public async Task<IEnumerable<CartItemViewModel>> GetAllFromCart(int cartId)
    {
        var dtos = await _cartItemService.GetAllFromCart(cartId);
        return dtos.Select(dto => new CartItemViewModel()
        {
            Id = dto.Id,
            CartId = dto.CartId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        }).ToList();
    }

    [HttpPost("CreateItem")]
    public CartItemViewModel Create([FromBody] CartItemViewModel cartItemViewModel)
    {
        var dto = new CartItemDto()
        {
            Id = cartItemViewModel.Id,
            CartId = cartItemViewModel.CartId,
            ProductId = cartItemViewModel.ProductId,
            Quantity = cartItemViewModel.Quantity
        };
        cartItemViewModel.Id = _cartItemService.Create(dto);
        return cartItemViewModel;
    }

    [HttpPut("UpdateItem")]
    public CartItemViewModel Update([FromBody] CartItemViewModel cartItemViewModel)
    {
        var dto = new CartItemDto()
        {
            Id = cartItemViewModel.Id,
            CartId = cartItemViewModel.CartId,
            ProductId = cartItemViewModel.ProductId,
            Quantity = cartItemViewModel.Quantity
        };
        _cartItemService.Update(dto);
        return cartItemViewModel;
    }

    [HttpDelete("DeleteItem{id:int}")]
    public void Delete(int id)
    {
        _cartItemService.Delete(id);
    }
}
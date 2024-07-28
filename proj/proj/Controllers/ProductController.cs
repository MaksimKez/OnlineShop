using BLL.Dtos;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using proj.ViewModels;

namespace proj.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service ?? throw new ArgumentException(nameof(service), "Service err");
    }

    [HttpGet("GetProductById")]
    public ProductViewModel GetById([FromBody] int id)
    {
        var dto = _service.Get(id);
        return new ProductViewModel
        {
            Id = dto.Id,
            ProductType = dto.ProductType,
            Description = dto.Description,
            PhotoUrl = dto.PhotoUrl,
            PricePerUnit = dto.PricePerUnit,
            StockQuantity = dto.StockQuantity
        };
    }

    [HttpGet("GetOutOfStockProducts")]
    public async Task<List<ProductViewModel>> GetOutOfStock()
    {
        var dtos = await _service.GetOutOfStock();
        var products = dtos.Select(dto => new ProductViewModel()
        {
            Id = dto.Id,
            ProductType = dto.ProductType,
            Description = dto.Description,
            PhotoUrl = dto.PhotoUrl,
            PricePerUnit = dto.PricePerUnit,
            StockQuantity = dto.StockQuantity
        });
        return products.ToList();
    }

    [HttpPost("CreateProduct")]
    public ProductViewModel Create([FromBody] ProductViewModel productViewModel)
    {
        var dto = new ProductDto()
        {
            Id = productViewModel.Id,
            ProductType = productViewModel.ProductType,
            Description = productViewModel.Description,
            PhotoUrl = productViewModel.PhotoUrl,
            PricePerUnit = productViewModel.PricePerUnit,
            StockQuantity = productViewModel.StockQuantity
        };
        productViewModel.Id = _service.Create(dto);
        return productViewModel;
    }

    [HttpPut("UpdateProduct")]
    public ProductViewModel Update([FromBody] ProductViewModel productViewModel)
    {
        _service.Update(new ProductDto()
        {
            Id = productViewModel.Id,
            ProductType = productViewModel.ProductType,
            Description = productViewModel.Description,
            PhotoUrl = productViewModel.PhotoUrl,
            PricePerUnit = productViewModel.PricePerUnit,
            StockQuantity = productViewModel.StockQuantity
        });
        return productViewModel;
    }
    [HttpDelete("DeleteProduct/{id:int}")]
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
    public void IncrementStock([FromBody]ProductViewModel productViewModel, [FromQuery] int count)
    {
        _service.IncrementStock(new ProductDto()
        {
            Id = productViewModel.Id,
            ProductType = productViewModel.ProductType,
            Description = productViewModel.Description,
            PhotoUrl = productViewModel.PhotoUrl,
            PricePerUnit = productViewModel.PricePerUnit,
            StockQuantity = productViewModel.StockQuantity
        }, count);
    }
    [HttpPatch("DecrementStock/{count:int}")]
    public void DecrementStock([FromBody]ProductViewModel productViewModel, [FromQuery] int count)
    {
        _service.DecrementStock(new ProductDto()
        {
            Id = productViewModel.Id,
            ProductType = productViewModel.ProductType,
            Description = productViewModel.Description,
            PhotoUrl = productViewModel.PhotoUrl,
            PricePerUnit = productViewModel.PricePerUnit,
            StockQuantity = productViewModel.StockQuantity
        }, count);
    }
}
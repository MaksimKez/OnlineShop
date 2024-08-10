using BLL.Dtos;
using proj.ViewModels;

namespace proj.Mappers;

public class ProductMapperVm : IMapperVMs<ProductViewModel, ProductDto>
{
    public ProductViewModel? MapToVm(ProductDto? dto)
    {
        if (dto is null) return null;
        return new ProductViewModel
        {
            Id = dto.Id,
            ProductType = dto.ProductType,
            Description = dto.Description,
            PhotoUrl = dto.PhotoUrl,
            PricePerUnit = dto.PricePerUnit,
            StockQuantity = dto.StockQuantity,
        };
    }

    public ProductDto? MapToDto(ProductViewModel? viewModel)
    {
        if (viewModel is null) return null;
        return new ProductDto
        {
            Id = viewModel.Id,
            ProductType = viewModel.ProductType,
            Description = viewModel.Description,
            PhotoUrl = viewModel.PhotoUrl,
            PricePerUnit = viewModel.PricePerUnit,
            StockQuantity = viewModel.StockQuantity,
        };
    }
}
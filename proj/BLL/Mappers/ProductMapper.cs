﻿using BLL.Dtos;
using DAL.Entities;

namespace BLL.Mappers;

public class ProductMapper : IMapper<ProductDto, ProductEntity
>
{
    public ProductDto MapToModel(ProductEntity entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            ProductType = entity.ProductType,
            Description = entity.Description,
            PhotoUrl = entity.PhotoUrl,
            PricePerUnit = entity.PricePerUnit,
            StockQuantity = entity.StockQuantity,
        };
    }

    public ProductEntity MapFromModel(ProductDto dto)
    {
        return new ProductEntity
        {
            Id = dto.Id,
            PricePerUnit = dto.PricePerUnit,
            ProductType = dto.ProductType,
            Description = dto.Description,
            PhotoUrl = dto.PhotoUrl,
            StockQuantity = dto.StockQuantity
        };
    }
}
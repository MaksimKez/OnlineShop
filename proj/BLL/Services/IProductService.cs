using BLL.Dtos;

namespace BLL.Services;

public interface IProductService : IService<ProductDto>
{
    Task<IQueryable<ProductDto>> GetOutOfStock();
    void DecrementStock(ProductDto dto, int count);
    void IncrementStock(ProductDto dto, int count);
}
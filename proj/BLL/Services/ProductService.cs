using BLL.Dtos;
using BLL.Mappers;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper<ProductDto, ProductEntity> _mapper;

    public ProductService(IProductRepository repository, IMapper<ProductDto, ProductEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public int Create(ProductDto dto)
    {
        var entity = _mapper.MapFromModel(dto);
        return _repository.Create(entity);
    }

    public ProductDto Get(int id)
    {
        return _mapper.MapToModel(_repository.Read(id));
    }

    public void Update(ProductDto dto)
    {
        _repository.Update(_mapper.MapFromModel(dto));
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public async Task<IQueryable<ProductDto>> GetOutOfStock()
    {
        var prodsOutOfStock = _repository.GetOutOfStock();
        return await Task.FromResult(prodsOutOfStock.Select(pr => _mapper.MapToModel(pr)));
    }

    public void DecrementStock(ProductDto dto, int count)
    {
        _repository.DecrementStock(_mapper.MapFromModel(dto), count);
    }

    public void IncrementStock(ProductDto dto, int count)
    {
        _repository.IncrementStock(_mapper.MapFromModel(dto), count);
    }
}
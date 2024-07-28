using BLL.Dtos;
using BLL.Mappers;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _repository;
    private readonly IMapper<CartItemDto, CartItemEntity> _mapper;

    public CartItemService(ICartItemRepository repository, IMapper<CartItemDto, CartItemEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public int Create(CartItemDto dto)
    {
        var entity = _mapper.MapFromModel(dto);
        return _repository.Create(entity);
    }

    public CartItemDto Get(int id)
    {
        return _mapper.MapToModel(_repository.Read(id));
    }

    public void Update(CartItemDto dto)
    {
        _repository.Update(_mapper.MapFromModel(dto));
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public async Task<IQueryable<CartItemDto>> GetAllFromCart(int cartId)
    {
        var items = _repository.GetAllFromCart(cartId);
        return await Task.FromResult(items.Select(item => _mapper.MapToModel(item)));
    }
}
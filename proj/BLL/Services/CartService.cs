using BLL.Dtos;
using BLL.Mappers;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper<CartDto, CartEntity> _mapper;

    public CartService(ICartRepository cartRepository, IMapper<CartDto, CartEntity> mapper)
    {
        _cartRepository = cartRepository ?? throw new ArgumentException("Repository error", nameof(cartRepository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public int Create(CartDto dto)
    {
        var entity = _mapper.MapFromModel(dto);
        return _cartRepository.Create(entity);
    }

    public CartDto Get(int id)
    {
        return _mapper.MapToModel(_cartRepository.Read(id));
    }

    public void Update(CartDto dto)
    {
        _cartRepository.Update(_mapper.MapFromModel(dto));
    }

    public void Delete(int id)
    {
        _cartRepository.Delete(id);
    }

    public CartDto EditDiscount(CartDto dto, double discount)
    {
       var cartEntity =  _cartRepository.EditDiscount(_mapper.MapFromModel(dto), discount);
       return _mapper.MapToModel(cartEntity);
    }
}
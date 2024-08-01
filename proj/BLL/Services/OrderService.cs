using Azure.Core;
using BLL.Dtos;
using BLL.Mappers;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper<OrderDto, OrderEntity> _mapper;

    public OrderService(IMapper<OrderDto, OrderEntity> mapper, IOrderRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
    }

    public int Create(OrderDto dto)
    {
        var entity = _mapper.MapFromModel(dto);
        return _repository.Create(entity);
    }

    public OrderDto Get(int? id)
    {
        return _mapper.MapToModel(_repository.Read(id));
    }

    public void Update(OrderDto dto)
    {
        _repository.Update(_mapper.MapFromModel(dto));
    }

    public void Delete(int? id)
    {
        _repository.Delete(id);
    }

    public IEnumerable<OrderDto> GetOrdersDelivered(int userId)
    {
        var orders =  _repository.GetOrdersDelivered(userId).ToList();
        return orders.Select(order => _mapper.MapToModel(order));
    }

    public IEnumerable<OrderDto> GetDeliveringOrders(int userId)
    {
        var orders =  _repository.GetDeliveringOrders(userId).ToList();
        return orders.Select(order => _mapper.MapToModel(order));
    }
}
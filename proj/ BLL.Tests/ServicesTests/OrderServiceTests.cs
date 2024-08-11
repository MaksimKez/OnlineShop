using AutoFixture;
using BLL.Dtos;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using Moq;

namespace BLL.Tests.ServicesTests;

public class OrderServiceTests
{
    private Mock<IMapper<OrderDto, OrderEntity>> _mapperMock;
    private Mock<IOrderRepository> _repositoryMock;
    private OrderEntity _orderEntity;
    private OrderDto _orderDto;
    private Fixture _fixture;
    private IOrderService _underTest;
    
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        // Replaces the default behavior that throws on circular references with one that omits them.
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(behavior => _fixture.Behaviors.Remove(behavior));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _orderEntity = _fixture.Create<OrderEntity>();
        _orderDto = new OrderDto
        {
            Id = _orderEntity.Id,
            PaymentDateTime = _orderEntity.PaymentDateTime,
            DeliveryDateTime = _orderEntity.DeliveryDateTime,
            UserId = _orderEntity.UserId,
        };

        _mapperMock = new Mock<IMapper<OrderDto, OrderEntity>>();
        _repositoryMock = new Mock<IOrderRepository>();

        _underTest = new OrderService(_mapperMock.Object, _repositoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _mapperMock = null;
        _repositoryMock = null;
        _orderDto = null;
        _orderEntity = null;
        _fixture = null;
        _underTest = null;
    }

    [Test]
    public void Create_ShouldCallRepositoryCreate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_orderDto)).Returns(_orderEntity);
        _repositoryMock.Setup(r => r.Create(_orderEntity)).Returns(_orderEntity.Id);

        // Act
        var result = _underTest.Create(_orderDto);

        // Assert
        _repositoryMock.Verify(r => r.Create(_orderEntity), Times.Once);
        Assert.That(result, Is.EqualTo(_orderEntity.Id));
    }

    [Test]
    public void Get_ShouldReturnOrderDto()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Read(_orderEntity.Id)).Returns(_orderEntity);
        _mapperMock.Setup(m => m.MapToModel(_orderEntity)).Returns(_orderDto);

        // Act
        var result = _underTest.Get(_orderEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Read(_orderEntity.Id), Times.Once);
        Assert.That(result, Is.EqualTo(_orderDto));
    }

    [Test]
    public void Update_ShouldCallRepositoryUpdate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_orderDto)).Returns(_orderEntity);

        // Act
        _underTest.Update(_orderDto);

        // Assert
        _repositoryMock.Verify(r => r.Update(_orderEntity), Times.Once);
    }

    [Test]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Act
        _underTest.Delete(_orderEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_orderEntity.Id), Times.Once);
    }

    [Test]
    public void GetOrdersDelivered_ShouldReturnOrderDtos()
    {
        // Arrange
        var orders = new List<OrderEntity> { _orderEntity }.AsQueryable();
        _repositoryMock.Setup(r => r.GetOrdersDelivered(_orderEntity.UserId)).Returns(orders);
        _mapperMock.Setup(m => m.MapToModel(_orderEntity)).Returns(_orderDto);

        // Act
        var result = _underTest.GetOrdersDelivered(_orderEntity.UserId);

        // Assert
        _repositoryMock.Verify(r => r.GetOrdersDelivered(_orderEntity.UserId), Times.Once);

        var orderDtos = result.ToArray();
        Assert.That(orderDtos, Has.Length.EqualTo(1));
        Assert.That(orderDtos.First(), Is.EqualTo(_orderDto));
    }

    [Test]
    public void GetDeliveringOrders_ShouldReturnOrderDtos()
    {
        // Arrange
        var orders = new List<OrderEntity> { _orderEntity }.AsQueryable();
        _repositoryMock.Setup(r => r.GetDeliveringOrders(_orderEntity.UserId)).Returns(orders);
        _mapperMock.Setup(m => m.MapToModel(_orderEntity)).Returns(_orderDto);

        // Act
        var result = _underTest.GetDeliveringOrders(_orderEntity.UserId);

        // Assert
        _repositoryMock.Verify(r => r.GetDeliveringOrders(_orderEntity.UserId), Times.Once);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(_orderDto, result.First());
    }
}
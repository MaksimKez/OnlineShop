using AutoFixture;
using BLL.Dtos;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using Moq;

namespace BLL.Tests.ServicesTests;

public class CartItemServiceTests
{
    private Mock<IMapper<CartItemDto, CartItemEntity>> _mapperMock;
    private Mock<ICartItemRepository> _repositoryMock;
    private CartItemEntity _cartItemEntity;
    private CartItemDto _cartItemDto;
    private Fixture _fixture;
    private ICartItemService _underTest;
    
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        // Replaces the default behavior that throws on circular references with one that omits them.
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(behavior => _fixture.Behaviors.Remove(behavior));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _cartItemEntity = _fixture.Create<CartItemEntity>();
        _cartItemDto = new CartItemDto
        {
            Id = _cartItemEntity.Id,
            CartId = _cartItemEntity.CartId,
            ProductId = _cartItemEntity.CartId,
            Quantity = _cartItemEntity.Quantity
        };

        _mapperMock = new Mock<IMapper<CartItemDto, CartItemEntity>>();
        _repositoryMock = new Mock<ICartItemRepository>();

        _underTest = new CartItemService(_repositoryMock.Object, _mapperMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _mapperMock = null;
        _repositoryMock = null;
        _cartItemDto = null;
        _cartItemEntity = null;
        _fixture = null;
        _underTest = null;
    }
    
    [Test]
    public void Create_ShouldCallRepositoryCreate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_cartItemDto)).Returns(_cartItemEntity);
        _repositoryMock.Setup(r => r.Create(_cartItemEntity)).Returns(_cartItemEntity.Id);

        // Act
        var result = _underTest.Create(_cartItemDto);

        // Assert
        _repositoryMock.Verify(r => r.Create(_cartItemEntity), Times.Once);
        Assert.AreEqual(_cartItemEntity.Id, result);
    }

    [Test]
    public void Get_ShouldReturnCartItemDto()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Read(_cartItemEntity.Id)).Returns(_cartItemEntity);
        _mapperMock.Setup(m => m.MapToModel(_cartItemEntity)).Returns(_cartItemDto);

        // Act
        var result = _underTest.Get(_cartItemEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Read(_cartItemEntity.Id), Times.Once);
        Assert.AreEqual(_cartItemDto, result);
    }

    [Test]
    public void Update_ShouldCallRepositoryUpdate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_cartItemDto)).Returns(_cartItemEntity);

        // Act
        _underTest.Update(_cartItemDto);

        // Assert
        _repositoryMock.Verify(r => r.Update(_cartItemEntity), Times.Once);
    }

    [Test]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Act
        _underTest.Delete(_cartItemEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_cartItemEntity.Id), Times.Once);
    }

    [Test]
    public async Task GetAllFromCart_ShouldReturnCartItemDtos()
    {
        // Arrange
        var cartItems = new List<CartItemEntity> { _cartItemEntity }.AsQueryable();
        _repositoryMock.Setup(r => r.GetAllFromCart(_cartItemEntity.CartId)).Returns(cartItems);
        _mapperMock.Setup(m => m.MapToModel(_cartItemEntity)).Returns(_cartItemDto);

        // Act
        var result = await _underTest.GetAllFromCart(_cartItemEntity.CartId);

        // Assert
        _repositoryMock.Verify(r => r.GetAllFromCart(_cartItemEntity.CartId), Times.Once);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(_cartItemDto, result.First());
    }
}

using AutoFixture;
using BLL.Dtos;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Moq;

namespace BLL.Tests.ServicesTests;

public class CartServiceTests
{
    private Mock<IMapper<CartDto, CartEntity>> _mapperMock;
    private Mock<ICartRepository> _repositoryMock;
    private CartEntity _cartEntity;
    private CartDto _cartDto;
    private Fixture _fixture;
    private ICartService _underTest;
    [SetUp]
    
    public void Setup()
    {
        _fixture = new Fixture();
        // Replaces the default behavior that throws on circular references with one that omits them.
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(behavior => _fixture.Behaviors.Remove(behavior));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _cartEntity = _fixture.Create<CartEntity>();
        _cartDto = new CartDto
        {
            Id = _cartEntity.Id,
            Discount = _cartEntity.Discount,
            TotalPrice = _cartEntity.TotalPrice
        };

        _mapperMock = new Mock<IMapper<CartDto, CartEntity>>();
        _repositoryMock = new Mock<ICartRepository>();

        _underTest = new CartService(_repositoryMock.Object, _mapperMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _mapperMock = null;
        _repositoryMock = null;
        _cartDto = null;
        _cartEntity = null;
        _fixture = null;
        _underTest = null;
    }
    
    [Test]
    public void Create_ShouldReturnNewCartId()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_cartDto)).Returns(_cartEntity);
        _repositoryMock.Setup(r => r.Create(_cartEntity)).Returns(_cartEntity.Id);

        // Act
        var result = _underTest.Create(_cartDto);

        // Assert
        result.Should().Be(_cartEntity.Id);
    }

    [Test]
    public void Get_ShouldReturnCartDto_WhenCartExists()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Read(_cartEntity.Id)).Returns(_cartEntity);
        _mapperMock.Setup(m => m.MapToModel(_cartEntity)).Returns(_cartDto);

        // Act
        var result = _underTest.Get(_cartEntity.Id);

        // Assert
        result.Should().NotBeNull().And.BeOfType<CartDto>().And.BeEquivalentTo(_cartDto);
    }

    [Test]
    public void Update_ShouldCallMapperAndRepositoryUpdate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_cartDto)).Returns(_cartEntity);

        // Act
        _underTest.Update(_cartDto);

        // Assert
        _mapperMock.Verify(m => m.MapFromModel(_cartDto), Times.Once);
        _repositoryMock.Verify(r => r.Update(_cartEntity), Times.Once);
    }

    [Test]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Act
        _underTest.Delete(_cartEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_cartEntity.Id), Times.Once);
    }

    [Test]
    public void EditDiscount_ShouldReturnUpdatedCartDto()
    {
        // Arrange
        double newDiscount = 0.2;
        var updatedCartEntity = new CartEntity { Id = 1, Discount = newDiscount };
        var updatedCartDto = new CartDto { Id = 1, Discount = newDiscount };

        _mapperMock.Setup(m => m.MapFromModel(_cartDto)).Returns(_cartEntity);
        _repositoryMock.Setup(r => r.EditDiscount(_cartEntity, newDiscount)).Returns(updatedCartEntity);
        _mapperMock.Setup(m => m.MapToModel(updatedCartEntity)).Returns(updatedCartDto);

        // Act
        var result = _underTest.EditDiscount(_cartDto, newDiscount);

        // Assert
        result.Should().NotBeNull().And.BeOfType<CartDto>().And.BeEquivalentTo(updatedCartDto);
    }

}
using AutoFixture;
using BLL.Dtos;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Moq;

namespace BLL.Tests.ServicesTests;

public class ProductServiceTests
{
    private Mock<IMapper<ProductDto, ProductEntity>> _mapperMock;
    private Mock<IProductRepository> _repositoryMock;
    private ProductEntity _productEntity;
    private ProductDto _productDto;
    private Fixture _fixture;
    private IProductService _underTest;
    
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        // Replaces the default behavior that throws on circular references with one that omits them.
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(behavior => _fixture.Behaviors.Remove(behavior));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _productEntity = _fixture.Create<ProductEntity>();
        _productDto = new ProductDto
        {
            Id = _productEntity.Id,
            ProductType = _productEntity.ProductType,
            Description = _productEntity.Description,
            PhotoUrl = _productEntity.PhotoUrl,
            PricePerUnit = _productEntity.PricePerUnit,
            StockQuantity = _productEntity.StockQuantity
        };

        _mapperMock = new Mock<IMapper<ProductDto, ProductEntity>>();
        _repositoryMock = new Mock<IProductRepository>();

        _underTest = new ProductService(_repositoryMock.Object, _mapperMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _mapperMock = null;
        _repositoryMock = null;
        _productDto = null;
        _productEntity = null;
        _fixture = null;
        _underTest = null;
    }
    
     [Test]
    public void Create_ShouldCallRepositoryCreate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_productDto)).Returns(_productEntity);
        _repositoryMock.Setup(r => r.Create(_productEntity)).Returns(_productEntity.Id);

        // Act
        var result = _underTest.Create(_productDto);

        // Assert
        _repositoryMock.Verify(r => r.Create(_productEntity), Times.Once);
        Assert.AreEqual(_productEntity.Id, result);
    }

    [Test]
    public void Get_ShouldReturnProductDto()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Read(_productEntity.Id)).Returns(_productEntity);
        _mapperMock.Setup(m => m.MapToModel(_productEntity)).Returns(_productDto);

        // Act
        var result = _underTest.Get(_productEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Read(_productEntity.Id), Times.Once);
        Assert.AreEqual(_productDto, result);
    }

    [Test]
    public void Update_ShouldCallRepositoryUpdate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_productDto)).Returns(_productEntity);

        // Act
        _underTest.Update(_productDto);

        // Assert
        _repositoryMock.Verify(r => r.Update(_productEntity), Times.Once);
    }

    [Test]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Act
        _underTest.Delete(_productEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_productEntity.Id), Times.Once);
    }

    [Test]
    public async Task GetOutOfStock_ShouldReturnProductDtos()
    {
        // Arrange
        var productsOutOfStock = new List<ProductEntity> { _productEntity }.AsQueryable();
        _repositoryMock.Setup(r => r.GetOutOfStock()).Returns(productsOutOfStock);
        _mapperMock.Setup(m => m.MapToModel(_productEntity)).Returns(_productDto);

        // Act
        var result = await _underTest.GetOutOfStock();

        // Assert
        _repositoryMock.Verify(r => r.GetOutOfStock(), Times.Once);
        var productDtos = result.ToArray();
        Assert.That(productDtos, Has.Length.EqualTo(1));
        Assert.That(productDtos.First(), Is.EqualTo(_productDto));
    }

    [Test]
    public void DecrementStock_ShouldCallRepositoryDecrementStock()
    {
        // Arrange
        int count = 5;
        _mapperMock.Setup(m => m.MapFromModel(_productDto)).Returns(_productEntity);

        // Act
        _underTest.DecrementStock(_productDto, count);

        // Assert
        _repositoryMock.Verify(r => r.DecrementStock(_productEntity, count), Times.Once);
    }

    [Test]
    public void IncrementStock_ShouldCallRepositoryIncrementStock()
    {
        // Arrange
        int count = 5;
        _mapperMock.Setup(m => m.MapFromModel(_productDto)).Returns(_productEntity);

        // Act
        _underTest.IncrementStock(_productDto, count);

        // Assert
        _repositoryMock.Verify(r => r.IncrementStock(_productEntity, count), Times.Once);
    }
}
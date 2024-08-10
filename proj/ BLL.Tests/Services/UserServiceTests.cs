using AutoFixture;
using BLL.Dtos;
using BLL.Mappers;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Moq;

namespace BLL.Tests;

public class UserServiceTest
{
    private Mock<IMapper<UserDto, UserEntity>> _mapperMock;
    private Mock<IUserRepository> _repositoryMock;
    private UserEntity _userEntity;
    private UserDto _userDto;
    private Fixture _fixture;
    private IUserService _underTest;
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        // Replaces the default behavior that throws on circular references with one that omits them.
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _userEntity = _fixture.Create<UserEntity>();
        _userDto = new UserDto
        {
            Id = _userEntity.Id,
            Username = _userEntity.Username,
            FirstName = _userEntity.FirstName,
            SecondName = _userEntity.SecondName,
            DateOfBirth = _userEntity.DateOfBirth,
            Email = _userEntity.Email,
            PhoneNumber = _userEntity.PhoneNumber,
            CartId = _userEntity.CartId
        };

        _mapperMock = new Mock<IMapper<UserDto, UserEntity>>();
        _repositoryMock = new Mock<IUserRepository>();

        _underTest = new UserService(_mapperMock.Object, _repositoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
    }
    
    [Test]
    public void Create_ShouldReturnNewUserId()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_userDto)).Returns(_userEntity);
        _repositoryMock.Setup(r => r.Create(_userEntity)).Returns(_userEntity.Id);

        // Act
        var result = _underTest.Create(_userDto);

        // Assert
        result.Should().Be(_userEntity.Id);
    }

    [Test]
    public void Get_ShouldReturnUserDto_WhenUserExists()
    {
        
        // Arrange
        var expected = _userDto;

        _repositoryMock.Setup(p => p.Read(It.IsAny<int>()))
            .Returns(_userEntity);

        _mapperMock.Setup(p => p.MapToModel(It.IsAny<UserEntity>()))
            .Returns(_userDto);
        
        // Act
        var result = _underTest.Get(_userEntity.Id);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .BeOfType<UserDto>()
            .And
            .BeEquivalentTo(expected);
    }
    
    [Test]
    public void Update_ShouldCallMapperAndRepositoryUpdate()
    {
        // Arrange
        _mapperMock.Setup(m => m.MapFromModel(_userDto)).Returns(_userEntity);

        // Act
        _underTest.Update(_userDto);

        // Assert
        _mapperMock.Verify(m => m.MapFromModel(_userDto), Times.Once);
        _repositoryMock.Verify(r => r.Update(_userEntity), Times.Once);
    }
    
    [Test]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Act
        _underTest.Delete(_userEntity.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_userEntity.Id), Times.Once);
    }
    
    [Test]
    public async Task GetAllWithoutOrders_ShouldReturnListOfUserDtos()
    {
        // Arrange
        var userEntities = _fixture.CreateMany<UserEntity>().AsQueryable();
        var userDtos = userEntities.Select(e => new UserDto
        {
            Id = e.Id,
            Username = e.Username,
            FirstName = e.FirstName,
            SecondName = e.SecondName,
            DateOfBirth = e.DateOfBirth,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            CartId = e.CartId
        }).ToList();

        _repositoryMock.Setup(r => r.GetAllWithoutOrders()).Returns(userEntities);
        _mapperMock.Setup(m =>
            m.MapToModel(It.IsAny<UserEntity>())).Returns((UserEntity e) => userDtos.First(d => d.Id == e.Id));

        // Act
        var result = await _underTest.GetAllWithoutOrders();

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(userDtos);
    }
}

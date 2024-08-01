using BLL.Dtos;
using BLL.Mappers;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper<UserDto, UserEntity> _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper<UserDto, UserEntity> mapper, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
    }

    public int Create(UserDto dto)
    {
        var entity = _mapper.MapFromModel(dto);
        return _userRepository.Create(entity);
    }

    public UserDto Get(int? id)
    {
        return _mapper.MapToModel(_userRepository.Read(id));
    }

    public void Update(UserDto dto)
    {
        _userRepository.Update(_mapper.MapFromModel(dto));
    }

    public void Delete(int? id)
    {
        _userRepository.Delete(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllWithoutOrders()
    {
        var users = _userRepository.GetAllWithoutOrders().ToList();
        return await Task.FromResult(users.Select(userEntity => _mapper.MapToModel(userEntity)).ToList());
    }
}
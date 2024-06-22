using proj.Models;
using proj.Models.DTO;

namespace proj.Repositories.IRepositories;

public interface IUserRepository
{
    bool IsValidUser(string username, string password, string phoneNumber = "", string email = "");
    
    bool IsUniqueUser(string username);
    
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    
    Task<User> RegisterAsync(RegistrationRequestDto registrationRequestDto);
}
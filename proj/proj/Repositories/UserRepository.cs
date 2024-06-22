using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL;
using Microsoft.IdentityModel.Tokens;
using proj.Mappers;
using proj.Models;
using proj.Models.DTO;
using proj.Repositories.IRepositories;

namespace proj.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserMapper _userMapper;
    private string secret;
    
    public UserRepository(ApplicationDbContext dbContext, IUserMapper userMapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _userMapper = userMapper;
        secret = configuration.GetValue<string>("ApiSettings:Secret") ?? string.Empty;
        if (secret == string.Empty)
            throw new Exception("ne rabotayet");
    }
    
    public bool IsValidUser(string username, string password, string phoneNumber = "", 
        string email = "")
    {
        if (username.Length < 5 || username.Contains(',') || username.Contains('.'))
            return false;
        
        if (email == String.Empty && phoneNumber == String.Empty)
            return false;
        
        return IsUniqueUser(username);
    }

    public bool IsUniqueUser(string username)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Username == username);
        return user is null;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = _userMapper.MapToUser(_dbContext.Users.FirstOrDefault(us =>
            us.Username.Equals(loginRequestDto.Username, StringComparison.CurrentCultureIgnoreCase)
            && us.Password.Equals(loginRequestDto.Password)) );

        if (user is null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        
        // as one contact (or both email or phone) is required, let's consider that phone number is more preferable
        var contactClaim = user.PhoneNumber != string.Empty
            ? new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            : new Claim(ClaimTypes.Email, user.Email);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                contactClaim
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new LoginResponseDto()
        {
            Token = tokenHandler.WriteToken(token),
            User = user
        };
    }

    public async Task<User> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        if (!IsValidUser(registrationRequestDto.Username, registrationRequestDto.Password,
                registrationRequestDto.PhoneNumber, registrationRequestDto.Email))
            return null;

        var user = _userMapper.MapToUser(registrationRequestDto, _dbContext);
        
        _dbContext.Users.Add(_userMapper.MapToEntity(user));
        await _dbContext.SaveChangesAsync();
        user.Password = string.Empty;
        return user;
    }
}
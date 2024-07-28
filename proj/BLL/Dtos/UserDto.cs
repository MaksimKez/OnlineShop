namespace BLL.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? CartId { get; set; }
}
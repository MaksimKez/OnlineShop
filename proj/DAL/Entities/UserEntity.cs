namespace DAL.Entities;

public class UserEntity
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? CartId { get; set; }
    public CartEntity Cart { get; set; }
    public ICollection<OrderEntity> Orders { get; set; }
}

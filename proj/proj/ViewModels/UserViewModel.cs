using System.ComponentModel.DataAnnotations;

namespace proj.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [StringLength(20, ErrorMessage = "First name is invalid :(")]
    public string FirstName { get; set; }
    [StringLength(20, ErrorMessage = "Second name is invalid :(")]
    public string SecondName { get; set; }

    public int? CartId { get; set; }

    [DataType(DataType.Date), Required(ErrorMessage = "Username is required")]
    public DateOnly DateOfBirth { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
}
using BLL.Dtos;
using FluentValidation;

namespace BLL.FluentValidation;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 50).WithMessage("First name must be less than 50 characters.");

        RuleFor(user => user.SecondName)
            .NotEmpty().WithMessage("Second name is required.")
            .Length(1, 50).WithMessage("Second name must be less than 50 characters.");

        RuleFor(user => user.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        RuleFor(user => user.CartId)
            .GreaterThan(0).When(user => user.CartId.HasValue)
            .WithMessage("CartId must be greater than 0 if provided.");
    }
}
using BLL.Dtos;
using FluentValidation;

namespace BLL.FluentValidation;

public class CartValidator: AbstractValidator<CartDto>
{
    public CartValidator()
    {
        RuleFor(cart => cart.Discount)
            .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");

        RuleFor(cart => cart.TotalPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Total price must be non-negative.");
    }
}
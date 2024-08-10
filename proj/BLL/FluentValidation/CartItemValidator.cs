using BLL.Dtos;
using FluentValidation;

namespace BLL.FluentValidation;

public class CartItemValidator: AbstractValidator<CartItemDto>
{
    public CartItemValidator()
    {
        RuleFor(cartItem => cartItem.CartId)
            .GreaterThan(0).WithMessage("Cart ID must be greater than 0.");

        RuleFor(cartItem => cartItem.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(cartItem => cartItem.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
using BLL.Dtos;
using FluentValidation;

namespace BLL.FluentValidation;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(product => product.ProductType)
            .IsInEnum().WithMessage("Invalid product type.");

        RuleFor(product => product.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be less than 500 characters.");

        RuleFor(product => product.PhotoUrl)
            .NotEmpty().WithMessage("Photo URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Invalid URL format.");

        RuleFor(product => product.PricePerUnit)
            .GreaterThan(0).WithMessage("Price per unit must be greater than 0.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be non-negative.");
    }
}
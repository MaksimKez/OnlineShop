using BLL.Dtos;
using FluentValidation;

namespace BLL.FluentValidation;

public class OrderValidator: AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(order => order.PaymentDateTime)
            .NotEmpty().WithMessage("Payment date and time is required.")
            .LessThan(order => order.DeliveryDateTime).WithMessage("Payment date must be before delivery date.");

        RuleFor(order => order.DeliveryDateTime)
            .NotEmpty().WithMessage("Delivery date and time is required.");

        RuleFor(order => order.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
    }
}
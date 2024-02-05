using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutCommandVlidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutCommandVlidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{UserName} is required")
                .NotEmpty().
                MaximumLength(50).WithMessage("{UserName} must not exceed 50 charter");

            RuleFor(p => p.EmailAddress)
                    .NotEmpty().WithMessage("{EmilAddress} is required");

            RuleFor(p => p.TotalPrice).NotEmpty().WithMessage("{UserName} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater then zero");
        }

    }
}

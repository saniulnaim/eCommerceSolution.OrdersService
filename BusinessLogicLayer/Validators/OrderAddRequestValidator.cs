using BusinessLogicLayer.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Validators
{
    public class OrderAddRequestValidator: AbstractValidator<OrderAddRequest>
    {
        public OrderAddRequestValidator()
        {
            RuleFor(x => x.UserID).NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order Date is required.");
            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Order items are required.");
        }
    }
}

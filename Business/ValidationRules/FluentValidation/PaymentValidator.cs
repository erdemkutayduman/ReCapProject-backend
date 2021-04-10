using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.NameSurname).NotEmpty();
            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.ExpirationMonth).NotEmpty();
            RuleFor(p => p.Cvc).NotEmpty();
        }
    }
}

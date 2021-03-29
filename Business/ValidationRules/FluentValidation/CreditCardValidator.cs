using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.NameSurname).NotEmpty();
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.Cvc).NotEmpty();
            RuleFor(c => c.ExpirationDate).NotEmpty();
        }
    }
}

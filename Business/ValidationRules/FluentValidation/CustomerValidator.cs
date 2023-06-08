using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p=>p.Email).NotEmpty().EmailAddress();
            RuleFor(p=>p.FirstName).NotEmpty();
            RuleFor(p=>p.LastName).NotEmpty();
            RuleFor(p=>p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).Must(PhoneNumberRule);
        }
        private bool PhoneNumberRule(string arg) => Regex.Match(arg, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$", RegexOptions.IgnoreCase).Success;
    }
}

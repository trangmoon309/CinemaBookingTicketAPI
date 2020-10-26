using Contracts.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Validation
{
    //Fluent Validator
    public class RegisterValidator : AbstractValidator<UserRegistrationRequest>
    {       
        public RegisterValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(PhoneNumberValid)
                .WithMessage("PhoneNumber have 10 numbers!");
        }
        
        private bool PhoneNumberValid(string num)
        {
            try
            {
                if (num.Length > 10 || num.Length < 10) return false;
                if (Convert.ToInt32(num) != 0 && Convert.ToInt32(num) > 0) return true;
                return false;
            }
            catch(Exception ee)
            {
                var errorMeassge = ee.Message;
                return false;
            }
        }
    }
}

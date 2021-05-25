using FeaturesAPI.Domain.Models;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class AddressValidator : AbstractValidator<AddressData>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Number)
             .NotNull()
             .WithMessage("Number cannot be null");

            RuleFor(x => x.ZipCode)
             .Must(BeAValidZipCode)
             .WithMessage("ZipCode it is not a valid.");

        }

        private bool BeAValidZipCode(string zipCode)
        {
            bool bvalid = false;

            if (zipCode != null)
            {
                Regex regex = new Regex(@"^\d{5}-\d{3}$");

                if (regex.IsMatch(zipCode))
                {
                    // zipCode válido
                    bvalid = true;
                }
            }

            return bvalid;
        }
    }
}

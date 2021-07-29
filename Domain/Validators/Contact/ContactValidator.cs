using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models = Domain.Models;

namespace Domain.Validators.Contact
{
    public class ContactValidator : AbstractValidator<Models.Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.IdClient)
             .Null()
             .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.Name)
             .Null()
             .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.Phone)
             .Null()
             .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.Status)
             .Null()
             .WithMessage("{PropertyName} cannot be null.");

            RuleFor(x => x.Phone)
             .Must(BeAValidPhone)
             .WithMessage("{PropertyName} it is not a valid phone.");


            RuleFor(x => x.Email)
             .Must(BeAValidEmail)
             .WithMessage("{PropertyName} it is not a valid email.");
        }

        private bool BeAValidEmail(string email)
        {

            bool bvalid = false;

            if (email == null || email == "")
            {
                bvalid = true;
            }
            else 
            {
                Regex regex = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (regex.IsMatch(email))
                {
                    // email válido
                    bvalid = true;
                }
            }

            return bvalid;
        }

        public bool BeAValidPhone(string phone)
        {
            bool bvalid = true;
            Regex regex = new Regex(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$");

            if (!regex.IsMatch(phone)) { bvalid = false; }

            return bvalid;
        }
    }
}

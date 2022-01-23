using FeaturesAPI.Domain.Models;
using FluentValidation;
using FeaturesAPI.Domain.Models.Enum;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class PeopleValidator : AbstractValidator<People>
    {
       public PeopleValidator()
        {
            RuleFor(x => x.LastName)
               .NotNull()
               .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Name)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            //RuleFor(x => x.Phone)
            //        .Must(BeAValidPhone)
            //        .WithMessage("{PropertyName} it is not a valid phone");

            RuleFor(x => x.User)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocType)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Email)
                    .Must(BeAValidEmail)
                    .WithMessage("{PropertyName}  it is not a valid email");

            RuleFor(x => x.Address)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x)
                    .Must(BeADocumentValid)
                    .WithMessage("{PropertyName} it is not a valid document");

        }

        private bool BeADocumentValid(People people)
        {
            var docNumber =  people.DocNumber;

            if (docNumber != null)
            {
                docNumber = docNumber.Trim();
                docNumber = docNumber.Replace(".", "").Replace("-", "").Replace("/", "");

                switch (docNumber.Length)
                {
                    case 11:
                        if (people.DocType == DocType.Cpf)
                            return IsCPF(docNumber);
                        else return false;
                    case 14:
                        if (people.DocType == DocType.Cnpj)
                            return IsCnpj(docNumber);
                        else return false;
                    default:
                        return false;
                }
            }
            return false;
        }

        public bool IsCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma =0;
            int resto;
           
            tempCpf = cpf.Substring(0, 9);

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
           
            soma = 0;
            
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            
            resto = soma % 11;
            
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            
            digito = digito + resto.ToString();
            
            return cpf.EndsWith(digito);
        }

        public bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public bool BeAValidEmail(string email)
        {         
            if (string.IsNullOrEmpty(email))
                return false;

            var regex = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (regex.IsMatch(email))
                return true;
            else
                return false;
        }

        public bool BeAValidPhone(string phone)
        {
            bool bvalid = true;
            //Regex regex = new Regex(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$");

            //if (!regex.IsMatch(phone)) { bvalid = false; }

            //phones.ToList().ForEach(phone => { if (!regex.IsMatch(phone)) { bvalid = false; } });

            return bvalid;
        }
    }
}

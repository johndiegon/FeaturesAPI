using FeaturesAPI.Domain.Models;
using FluentValidation;
using System.Collections.Generic;

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

            RuleFor(x => x.Phone)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.User)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocNumber)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocType)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Email)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.Address)
                    .NotNull()
                    .WithMessage("{PropertyName} cannot be null");

            RuleFor(x => x.DocNumber)
                    .Must(BeADocumentValid)
                    .WithMessage("{PropertyName} it is not a valid document");

        }

        private bool BeADocumentValid(string DocNumber)
        {
            DocNumber = DocNumber.Trim();
            DocNumber = DocNumber.Replace(".", "").Replace("-", "").Replace("/", "");
            
            switch ( DocNumber.Length)
            {
                case 11:
                    return IsCPF(DocNumber);
                case 14:
                    return IsCnpj(DocNumber);
                default:
                    return false;
            }
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
    }
}

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Application.Helpers
{
    public static class StringExtensions
    {
        private static readonly Lazy<Regex> BeAnEmailRegex = new Lazy<Regex>(() => new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$"));

        public static bool IsCpf(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            cpf = cpf.RemoveFormatting();
            cpf = cpf.FillWithZerosLeft(11);

            if (cpf.Any(ch => !char.IsNumber(ch)))
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf == "00000000000" || cpf == "11111111111" ||
                    cpf == "22222222222" || cpf == "33333333333" ||
                    cpf == "44444444444" || cpf == "55555555555" ||
                    cpf == "66666666666" || cpf == "77777777777" ||
                    cpf == "88888888888" || cpf == "99999999999")
            {
                return false;
            }

            var total = 0;
            var mod = 0;
            for (var i = 0; i < 9; i++)
            {
                var current = cpf[i];
                total += int.Parse(current.ToString()) * (i + 1);
            }
            mod = total % 11;

            if (mod == 10)
            {
                mod = 0;
            }

            if (mod.ToString() != cpf[9].ToString())
            {
                return false;
            }

            total = 0;
            for (var i = 0; i < 10; i++)
            {
                var current = cpf[i];
                total += int.Parse(current.ToString()) * i;
            }
            mod = total % 11;

            if (mod == 10)
            {
                mod = 0;
            }

            return mod.ToString() == cpf[10].ToString();
        }

        public static string RemoveFormatting(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = Regex.Replace(input, @"[A-Za-z./-]", "");
            input = Regex.Replace(input, "\\s+", "");

            return input;
        }

        public static bool IsCNPJ(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool BeAnEmail(this string input)
        {
            if (input == null)
                return false;

            return BeAnEmailRegex.Value.IsMatch(input.Trim());
        }

        public static bool BeAPassword(this string input)
        {
            if (input == null)
                return false;

            if (input.Length != 8)
                return false;

            if (input.Count(char.IsUpper) == 0)
                return false;

            if (input.Count(char.IsLower) == 0)
                return false;

            if (input.Count(char.IsNumber) == 0)
                return false;

            if (input.Count(char.IsSymbol) > 0)
                return false;

            if (input.Count(char.IsWhiteSpace) > 0)
                return false;

            return true;
        }

        private static readonly Lazy<Regex> BeAPostalCodeRegex = new Lazy<Regex>(() => new Regex(@"^\d{5}((-)?\d{3})"));

        public static bool IsInteger(this string arg)
        {
            return !string.IsNullOrWhiteSpace(arg) && arg.All(char.IsNumber);
        }

        public static string FillWithZerosLeft(this string input, int limit)
        {
            return input.PadLeft(limit, '0');
        }

        static readonly string[] ones = new string[] { "", "Um", "Dois", "Tres", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
        static readonly string[] teens = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezeseis", "Dezesete", "Dezoito", "Dezenove" };
        static readonly string[] tens = new string[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
        static readonly string[] thousandsGroups = { "", " Mil", " Milhão", " Bilhão" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, ones[n / 100] + " Cem", 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
                if (n % 1000 == 0)
                {
                    return friendlyInt;
                }
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(this int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negativo " + (-n).IntegerToWritten();
            }

            return FriendlyInteger(n, "", 0);
        }

        public static string GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            semente = semente + resto;
            return semente;
        }

        public static string EncryptSha256Hash(this string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

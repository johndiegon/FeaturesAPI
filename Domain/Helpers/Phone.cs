using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class Phone
    {
        public static string TakeAValidNumber(string phone)
        {
            phone = phone.Replace(@"\s", "");
            phone = phone.Replace(@"+", "");
            phone = phone.Replace(@"-", "");
            phone = phone.Replace(@"(", "");
            phone = phone.Replace(@")", "");
            phone = phone.Replace(" ", "");

            switch (phone.Length)
            {
                case 13:
                    return phone;
                case 11:
                    return string.Concat("55", phone);
                default:
                    return string.Empty;
            }
        }
    }
}

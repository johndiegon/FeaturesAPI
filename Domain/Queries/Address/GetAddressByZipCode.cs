using Domain.Validators;
using MediatR;
using System.Text.RegularExpressions;

namespace Domain.Queries.Address
{
    public class GetAddressByZipCode : Validate, IRequest<GetAddressResponse>
    {
        public string ZipCode { get; set; }
        public override bool IsValid()
        {
            return BeAValidZipCode(ZipCode);
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

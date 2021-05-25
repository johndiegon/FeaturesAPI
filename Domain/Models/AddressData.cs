using Domain.Validators;

namespace FeaturesAPI.Domain.Models
{
    public class AddressData : Validate
    {
        public string Address { get; set; }
        public string District { get; set; }
        public string Uf { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddressValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

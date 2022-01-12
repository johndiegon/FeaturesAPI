using Domain.Validators;
using FeaturesAPI.Domain.Models.Enum;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class People : Validate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public DocType DocType { get; set; }
        public string Email { get; set; }
        public AddressData Address { get; set; }
        public List<string> Phone { get; set; }
        public string IdUser { get; set; }
        public UserModel User { get; set; }
        public EntityStatus Status  { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new PeopleValidator().Validate(this);
            this.Address.ValidationResult = new AddressValidator().Validate(this.Address);
            this.User.ValidationResult = new UserValidator().Validate(this.User);
            ValidationResult.Errors.AddRange(Address.ValidationResult.Errors);
            ValidationResult.Errors.AddRange(User.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }  
}

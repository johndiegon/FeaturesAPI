using Domain.Helpers;
using Domain.Validators;
using FeaturesAPI.Domain.Models.Enum;
using System.Collections.Generic;

namespace FeaturesAPI.Domain.Models
{
    public class People : Validate
    {
        private List<string> _phones;
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public string AnswerDefault { get; set; }
        public DocType DocType { get; set; }
        public string Email { get; set; }
        public AddressData Address { get; set; }
        public bool IsASubscriber   { get; set; }
        public List<string> AskToQuit { get; set; }
        public string AnswerToQuit { get; set; }
        public List<string> Phone
        {
            get { return _phones; }
            set 
            {
                _phones = new List<string>();
                value.ForEach(p => _phones.Add(PhoneValid.TakeAValidNumber(p)));
            }
        }
        public string DefaultAnswer { get; set; }
        public string IdUser { get; set; }
        public UserModel User { get; set; }
        public EntityStatus Status  { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new PeopleValidator().Validate(this);
            this.Address.ValidationResult = new AddressValidator().Validate(this.Address);
            //this.User.ValidationResult = new UserValidator().Validate(this.User);
            ValidationResult.Errors.AddRange(Address.ValidationResult.Errors);
            //ValidationResult.Errors.AddRange(User.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }  
}

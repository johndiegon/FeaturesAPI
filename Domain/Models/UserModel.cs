using Domain.Validators;

namespace FeaturesAPI.Domain.Models
{
    public class UserModel : Validate
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsConfirmedEmail { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UserValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
  
}

using FluentValidation.Results;

namespace Domain.Validators
{
    public abstract class Validate
    {
        public Validate()
        {
            
        }
        internal ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}

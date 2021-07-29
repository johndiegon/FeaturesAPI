using Domain.Validators;
using MediatR;

namespace Domain.Commands.TypeList.Post
{
    public class PostTypeListCommand : Validate, IRequest<PostTypeListCommandResponse>
    {
        public Models.TypeList TypeList { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PostTypeListValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

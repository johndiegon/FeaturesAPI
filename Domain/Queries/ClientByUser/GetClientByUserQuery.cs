using Domain.Validators;
using Domain.Validators.Client;
using MediatR;

namespace Domain.Queries.ClientByUser
{
    public class GetClientByUserQuery : Validate, IRequest<GetClientByUserQueryResponse>
    {
        public string IdUser { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetClientByUserQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

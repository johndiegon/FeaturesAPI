using Domain.Validators;
using Domain.Validators.Client;
using MediatR;

namespace Domain.Queries.Client
{
    public class GetClientQuery : Validate, IRequest<GetClientQueryResponse>
    {
        public string IdUser{ get; set; }
        public override bool IsValid()
        {
            ValidationResult = new GetClientQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

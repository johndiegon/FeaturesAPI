using Domain.Queries.Client;
using FluentValidation;

namespace Domain.Validators.Client
{

    public class GetClientQueryValidator : AbstractValidator<GetClientQuery>
    {
        public GetClientQueryValidator()
        {
            RuleFor(x => x.IdUser).NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}

using Domain.Queries.ClientByUser;
using FluentValidation;

namespace Domain.Validators.Client
{

    public class GetClientByUserQueryValidator : AbstractValidator<GetClientByUserQuery>
    {
        public GetClientByUserQueryValidator()
        {
            RuleFor(x => x.IdUser).NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}

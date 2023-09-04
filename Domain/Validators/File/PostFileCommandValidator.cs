using Domain.Commands.File.Post;
using FluentValidation;
using System.IO;

namespace Domain.Validators.File
{
    public class PostFileCommandValidator : AbstractValidator<PostFileCommand>
    {
        public PostFileCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");

            //RuleFor(x => x.File.FileName)
            //   .Must(BeAValidExtension)
            //   .WithMessage("Extension is not valid.");

            RuleFor(x => x.IdUser)
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.");
        }

        private bool BeAValidExtension(string fileName)
        {
            var extesion = Path.GetExtension(fileName).Replace(".", "");

            return extesion.ToUpper() == "CSV" ? true : false;
        }

    }

}
using Domain.Validators;
using Domain.Validators.File;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Commands.File.Post
{
    public class PostFileCommand : Validate, IRequest<PostFileCommandResponse>
    {
        public IFormFile File { get; set; }
        public string IdClient {get;set;}
        public override bool IsValid()
        {
            ValidationResult = new PostFileCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

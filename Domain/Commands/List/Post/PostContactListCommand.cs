using Domain.Commands.List.Post;
using Domain.Models;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.List
{
    public class PostContactListCommand : Validate , IRequest<PostContactListCommandResponse>
    {
        public ContactList ContactList { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new PostContactListValidator().Validate(this);
            ValidationResult.Errors.AddRange(ContactList.ValidationResult.Errors);
            return ValidationResult.IsValid;
        }
    }
}

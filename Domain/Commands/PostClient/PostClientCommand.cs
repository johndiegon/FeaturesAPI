using Domain.Commands.PostClient;
using Domain.Models;
using Domain.Validators;
using Domain.Validators.Client;
using FeaturesAPI.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Clients
{
    public class PostClientCommand : Validate , IRequest<PostClientCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocNumber { get; set; }
        public string DocType { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }
        public IEnumerable<string> Phone { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new PostClientCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

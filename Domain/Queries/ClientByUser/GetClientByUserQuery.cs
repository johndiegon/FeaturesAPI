using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries.ClientByUser
{
    public class GetClientByUserQuery : Validate, IRequest<GetClientByUserQueryResponse>
    {
        public string IdUser { get; set; }
    }
}

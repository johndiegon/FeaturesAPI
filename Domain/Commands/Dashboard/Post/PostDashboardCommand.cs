using Domain.Models;
using Domain.Validators;
using MediatR;


namespace Domain.Commands.Dashboard
{
    public class PostDashboardCommand : Validate, IRequest<CommandResponse>
    {
        public DataDashboard Dashboard { get; set; }
  
    }
}

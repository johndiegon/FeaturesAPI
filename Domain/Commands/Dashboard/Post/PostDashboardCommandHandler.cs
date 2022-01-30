using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Dashboard
{
    public class PostDashboardCommandHandler : IRequestHandler<PostDashboardCommand, CommandResponse>
    {
        private readonly IDataDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public PostDashboardCommandHandler( IDataDashboardRepository dataDashboard
                                          , IMapper mapper
                                          )
        {
            _mapper = mapper;
           _dashboardRepository = dataDashboard;
        }
        public async Task<CommandResponse> Handle(PostDashboardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dash = _mapper.Map<DataDashboardEntity>(request.Dashboard);
                _dashboardRepository.Create(dash);
                return await Task.FromResult(new CommandResponse()
                {
                    Data = new Data
                    {
                        Message = "Dasboar Criado com Sucesso",
                        Status = Status.Sucessed
                    }
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private CommandResponse GetResponseErro(string Message)
        {
            return new CommandResponse
            {
                Data = new Data
                {
                    Message = Message,
                    Status = Status.Error
                }
            };
        }
    }
}

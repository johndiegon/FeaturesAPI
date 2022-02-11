using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.Dashboard.Get
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, GetDashboardQueryResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IDataDashboardRepository _dataDashboardRepostory;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler(IClientRepository clientRepository
                                     , IMapper mapper
                                 , IDataDashboardRepository dataDashboardRepostory

                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _dataDashboardRepostory = dataDashboardRepostory;
        }

        public async Task<GetDashboardQueryResponse> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetDashboardQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var dashs = _dataDashboardRepostory.GetByClient(client.Id);

                   if (dashs.Any())
                    {
                        DateTime datelastDash = dashs.Select(d => d.DateTime).Max();
                        var lastDash = dashs.Where(d => d.DateTime == datelastDash).FirstOrDefault();
                        response.DataDashboard = _mapper.Map<DataDashboard>(lastDash);
                    }
                  
                    response.Data = new Data
                    {
                        Status = Status.Sucessed
                    };
                }

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetDashboardQueryResponse GetResponseErro(string Message)
        {
            return new GetDashboardQueryResponse
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

using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Queries.FileInput.Get
{
    public class GetHistorysFileQueryHandler : IRequestHandler<GetHistorysFileQuery, GetHistorysFileQueryResponse>
    {
        private readonly IReportFileRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetHistorysFileQueryHandler(IReportFileRepository repository,
                                           IClientRepository clientRepository,
                                           IMapper mapper
                                            )
                                          
        {
            _repository = repository;
            _clientRepository = clientRepository;  
            _mapper = mapper;   

        }
        public async Task<GetHistorysFileQueryResponse> Handle(GetHistorysFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetHistorysFileQueryResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var reportFile = _repository.GetByClientId(client.Id).ToList();

                    var responseReport = _mapper.Map<List<ReportFile>>(reportFile);

                    response = new GetHistorysFileQueryResponse
                    {
                        ReportFile = responseReport,
                        Data = new Data
                        {
                            Status = Status.Sucessed
                        }
                    };
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
            }
        }

        private GetHistorysFileQueryResponse GetResponseErro(string Message)
        {
            return new GetHistorysFileQueryResponse
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

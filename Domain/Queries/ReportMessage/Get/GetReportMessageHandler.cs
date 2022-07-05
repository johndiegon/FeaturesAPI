using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Models.ReportMessages;

namespace Domain.Queries.ReportMessage.Get
{
    public class GetReportMessageHandler : IRequestHandler<GetReportMessageQuery, GetReportMessageResponse>
    {
        private readonly IReportMessageRepository _reportMessageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetReportMessageHandler(IReportMessageRepository reportMessageRepository
                                     , IMapper mapper
                                     , IMediator mediator
                                     , IClientRepository clientRepository
                                     )
        {
            _reportMessageRepository = reportMessageRepository;
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
        }
        public async Task<GetReportMessageResponse> Handle(GetReportMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetReportMessageResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                    var client = _clientRepository.GetByUser(request.IdUser).FirstOrDefault();
                    var report = _reportMessageRepository.GetByClientId(client.Id).ToList();

                    if (report != null)
                    {
                        var reportMessages = report.Select(x => x.Template).ToList();

                        foreach (var r in reportMessages) 
                        {
                            var templateReport = new ReportMessages
                            {
                                Template = r,
                            };

                            var rps = report.Where(rp => rp.Template == r).ToList();

                            templateReport.TotalAnswer = rps.Select(rp => rp.HistoryAnswers.Count()).Sum();
                            
                            var listToProcess = new List<HistoryAnswer>();

                            foreach (var rp in rps)
                            {
                                rp.HistoryAnswers.ForEach(x => listToProcess.Add(new HistoryAnswer { Answer = x.Answer, Month = x.DateTime.ToString("yyyyMM")}));
                                templateReport.TotalSenders += rp.HistorySenders.Select(h => h.Count).Sum();
                            }

                            var answers = listToProcess.GroupBy(a => a.Answer)
                               .Select(a => new HistoryAnswer { Count = a.Count(), Answer = a.Key }).ToList();

                            templateReport.Answer = answers;
                        }
                    }

                    response.Data = new Data
                    {
                        Message = "Report Criada/Atualizado com sucesso!",
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
        private GetReportMessageResponse GetResponseErro(string Message)
        {
            return new GetReportMessageResponse
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

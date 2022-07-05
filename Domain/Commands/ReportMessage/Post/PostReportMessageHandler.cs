using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Infrastructure.Data.Entities.ReportMessageEntity;

namespace Domain.Commands.ReportMessage.Post
{
    public class PostReportMessageHandler : IRequestHandler<PostReportMessageCommand, CommandResponse>
    {
        private readonly IReportMessageRepository _reportMessageRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostReportMessageHandler(IReportMessageRepository reportMessageRepository
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
        public async Task<CommandResponse> Handle(PostReportMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CommandResponse();
                if (!request.IsValid())
                {
                    response = GetResponseErro("The request is invalid.");
                    response.Notification = request.Notifications();
                }
                else
                {
                     var report = _reportMessageRepository
                                       .GetByClientId(request.IdClient)
                                       .Where( r => r.Template == request.Template &&
                                                    r.Month == DateTime.Now.ToString("yyyyMM")
                                             )
                                       .FirstOrDefault();

                    if (report != null)
                    {
                        if (report.HistoryAnswers == null)
                            report.HistoryAnswers = new List<HistoryAnswerEntity>();

                        foreach (var item in request.HistoryAnswers)
                        {
                            report.HistoryAnswers.Add(_mapper.Map<HistoryAnswerEntity>(item));
                        }

                        if (report.HistorySenders == null)
                            report.HistorySenders = new List<HistorySenderEntity>();

                        foreach (var item in request.HistorySenders)
                        {
                            report.HistorySenders.Add(_mapper.Map<HistorySenderEntity>(item));
                        }

                        _reportMessageRepository.Create(_mapper.Map<ReportMessageEntity>(report));

                    }
                    else
                    {
                        var entity = new ReportMessageEntity
                        {
                            ClientID = request.IdClient,
                            Month = DateTime.Now.ToString("yyyyMM"),
                            Template = request.Template,
                            HistoryAnswers = request.HistoryAnswers != null ? _mapper.Map<List<HistoryAnswerEntity>>(request.HistoryAnswers) : null,
                            HistorySenders = request.HistorySenders != null ? _mapper.Map<List<HistorySenderEntity>>(request.HistorySenders) : null
                        };

                        _reportMessageRepository.Update(_mapper.Map<ReportMessageEntity>(report));
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

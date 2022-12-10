using AutoMapper;
using Domain.Models;
using Infrastructure.Data.Entities;
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
        private readonly IReportSendersRepository _reportSendersRepository;
        private readonly IReportAnswerRepository _reportAnswerRepository;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler( IClientRepository clientRepository
                                       , IMapper mapper
                                       , IReportSendersRepository reportSendersRepository
                                       , IReportAnswerRepository reportAnswerRepository
                                     )
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _reportAnswerRepository = reportAnswerRepository;
            _reportSendersRepository = reportSendersRepository;
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
                    var senders = _reportSendersRepository.GetByClientId(client.Id).ToList();
                    var answers = _reportAnswerRepository.GetByClientId(client.Id).ToList();

                    var dash = new DataDashboard() 
                    {
                        CountReceiverAnswer = answers.Count,
                        CountSendMessage = senders.Select( s => s.Count).Sum(),
                        CountReceiverAnswerThisMonth = answers.Where( a => a.DateTime.Month == DateTime.Now.Month &&
                                                                           a.DateTime.Year == DateTime.Now.Year
                                                                           ).Count(),
                        CountSendMessageThisMonth = senders.Where(a => a.DateTime.Month == DateTime.Now.Month &&
                                                                          a.DateTime.Year == DateTime.Now.Year)
                                                           .Select( s => s.Count).Sum(),
                    };

                    if(senders != null && senders.Count() > 0)
                    {
                        var templates = senders.Select(s => s.Template).Distinct().ToList();
                        
                        foreach (var template in templates)
                        {
                            var templateSends = (from send in senders
                                                 where send.Template == template
                                                 group send by new { DateTime = send.DateTime.ToShortDateString(), send.Count, send.CountOK } into newSenders
                                                 select new ReportSendEntity
                                                 {
                                                     Template = template,
                                                     DateTime = Convert.ToDateTime(newSenders.Key.DateTime),
                                                     Count = newSenders.Key.Count * newSenders.Count(),
                                                     CountOK = newSenders.Key.CountOK * newSenders.Count()
                                                 }).ToList();

                            var reportTemplate = new ReportTemplate()
                            {
                                Template = template,
                                HistorySenders = templateSends,
                                CountReceiverAnswer = answers.Where(a => a.Template == template).Count(),
                                CountSendMessage = senders.Where(a => a.Template == template).Select(s => s.Count).Sum(),
                                CountReceiverAnswerThisMonth = answers.Where(a => a.DateTime.Month == DateTime.Now.Month &&
                                                                                  a.DateTime.Year == DateTime.Now.Year &&
                                                                                  a.Template == template
                                                                           ).Count(),
                                CountSendMessageThisMonth = senders.Where(a => a.DateTime.Month == DateTime.Now.Month &&
                                                                               a.DateTime.Year == DateTime.Now.Year &&
                                                                               a.Template == template)
                                                                   .Select(s => s.Count).Sum(),
                            };

                            var totalAnswer = (from answer in answers
                                               where answer.Template == template
                                               group answer by new
                                               {
                                                   Template = answer.Template,
                                                   Answer = answer.Answer,
                                                   Month = answer.DateTime.Month,
                                                   Year = answer.DateTime.Year,
                                               } into atm
                                               select new Answers
                                               {
                                                   Month = atm.Key.Month.ToString(),
                                                   Year = atm.Key.Year.ToString(),
                                                   Template = atm.Key.Template,
                                                   Answer = atm.Key.Answer,
                                                   Count = atm.Count()
                                               }).ToList();

                            reportTemplate.TotalAnswer = totalAnswer;

                            var totalSenders = (from answer in senders
                                                where answer.Template == template
                                                group answer by new
                                                {
                                                    Template = answer.Template,
                                                    Month = answer.DateTime.Month,
                                                    Year = answer.DateTime.Year,
                                                } into atm
                                                select new Senders
                                                {
                                                    Month = atm.Key.Month.ToString(),
                                                    Year = atm.Key.Year.ToString(),
                                                    Template = atm.Key.Template,
                                                    Count = atm.Sum(a => a.Count),
                                                    CountOK = atm.Sum(a => a.CountOK),
                                                }).ToList();

                            reportTemplate.TotalSenders = totalSenders;

                            dash.ReportTemplates.Add(reportTemplate);
                        }
                    }
                  
                    response.Data = new Data
                    {
                        Status = Status.Sucessed
                    };
                    
                    response.DataDashboard = dash;
                }

                return await System.Threading.Tasks.Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return await System.Threading.Tasks.Task.FromResult(GetResponseErro(ex.Message));
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

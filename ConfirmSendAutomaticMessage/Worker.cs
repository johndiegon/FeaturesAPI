using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Enum;
using Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfirmSendAutomaticMessage
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMessagesDefaultRepository _messagesDefaultRepository;
        private readonly ITopicServiceBuss _topicServices;

        public Worker(
            ILogger<Worker> logger,
            ICalendarRepository calendarRepository, 
            IClientRepository clientRepository,
            IMessagesDefaultRepository messagesDefaultRepository,
            ITopicServiceBuss topicService
            )
        {
            _logger = logger;
            _calendarRepository = calendarRepository;
            _clientRepository = clientRepository;
            _topicServices = topicService;
            _messagesDefaultRepository = messagesDefaultRepository;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var begin = DateTime.Now.Minute - 5;
            var end = DateTime.Now.Minute + 5;
            var clientIds = _calendarRepository.GetClientsProcessAutomaticMessage(begin , end, StatusTask.Pending);
          
            foreach(var clientId in clientIds)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Process Client: {client}", clientId);

                var tasks = _calendarRepository.GetAutomaticMessage(clientId, begin, end, StatusTask.Pending);
                var client = _clientRepository.Get(clientId);
                var template = _messagesDefaultRepository.GetByClientId(clientId).Where(m => m.Title == "diretoApp_ConfirmSendMessage").FirstOrDefault();

                foreach (var task in tasks)
                {
                    var messageObject = new
                    {
                        Template = "diretoApp_ConfirmSendMessage",
                        IdClient = clientId,
                        Phone = client.Phone.FirstOrDefault(),
                        Params = JsonConvert.DeserializeObject(task.Params),
                        Name = client.Name,
                        PhoneTo = client.AdministratorPhone,
                    };

                    await _topicServices.SendMessage(messageObject, "sendMessageToList");
                }

            }
        }
    }
}

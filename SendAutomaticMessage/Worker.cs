using Infrasctuture.Service.Interfaces;
using Infrastructure.Data.Entities;
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

namespace SendAutomaticMessage
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMessagesDefaultRepository _messagesDefaultRepository;
        private readonly ITopicServiceBuss _topicServices;
        private readonly IContactRepository _contactRepository;

        public Worker(
            ILogger<Worker> logger,
            ICalendarRepository calendarRepository,
            IClientRepository clientRepository,
            IMessagesDefaultRepository messagesDefaultRepository,
            ITopicServiceBuss topicService,
            IContactRepository contactRepository
            )
        {
            _logger = logger;
            _calendarRepository = calendarRepository;
            _clientRepository = clientRepository;
            _topicServices = topicService;
            _messagesDefaultRepository = messagesDefaultRepository;
            _contactRepository = contactRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var begin = DateTime.Now.Minute - 5;
            var end = DateTime.Now.Minute + 5;
            var clientIds = _calendarRepository.GetClientsProcessAutomaticMessage(begin, end, StatusTask.Aproved);

            foreach (var clientId in clientIds)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Process Client: {client}", clientId);

                var tasks = _calendarRepository.GetAutomaticMessage(clientId, begin, end, StatusTask.Aproved);
                var client = _clientRepository.Get(clientId);

                var template = _messagesDefaultRepository.GetByClientId(clientId).Where(m => m.Title == "diretoApp_ConfirmSendMessage").FirstOrDefault();

                foreach (var task in tasks)
                {
                    var filters = JsonConvert.DeserializeObject<List<Param>>(task.Filters);
                    var paramaters = JsonConvert.DeserializeObject<List<Param>>(task.Params);

                    var contacts = _contactRepository.GetByClient(client.Id, filters).Result.ToList();

                    var url = GetParam(paramaters, "image");
                    if (string.IsNullOrEmpty(url))
                        url = GetParam(paramaters, "video");

                    foreach (var contact in contacts)
                    {
                        var messageObject = new
                        {
                            Template = task.Template,
                            IdClient = client.Id,
                            Phone = client.Phone.FirstOrDefault(),
                            Params = paramaters,
                            Name = contact.Name,
                            PhoneTo = contact.Phone,
                            UrlMedia = url
                        };

                        await _topicServices.SendMessage(messageObject, "sendMessageToList");
                    }
                }
            }
        }

        private static string GetParam(List<Param> paramaters, string nameParam)
        {
            var search = paramaters.Where(p => p.Name == nameParam).Select(pm => pm.Value).ToList();

            if (search.Count() > 0)
                return search[0] == null ? null : search[0].ToString();
            return null;
        }
    }
}

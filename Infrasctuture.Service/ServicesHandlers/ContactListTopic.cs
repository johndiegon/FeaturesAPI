﻿using Azure.Messaging.ServiceBus;
using Infrasctuture.Service.Contracts;
using Infrasctuture.Service.Interfaces;
using Infrasctuture.Service.Interfaces.settings;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrasctuture.Service.ServicesHandlers
{
    public class ContactListTopic : ITopicServiceBuss
    {
        ITopicSettings _topicSettings;
        public ContactListTopic(ITopicSettings topicSettings) 
        {
            _topicSettings = topicSettings;
        }

        public async Task<ImportedFile> SendMessage(ImportedFile orderList, string topic)
        {
            var client = new ServiceBusClient(_topicSettings.ConnectionString);
            var sender = client.CreateSender(topic);

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            string orderListJS = JsonSerializer.Serialize(orderList);

            if (!messageBatch.TryAddMessage(new ServiceBusMessage(orderListJS)))
            {
                // if it is too large for the batch
                throw new Exception($"The message is too large to fit in the batch.");
            }

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus topic
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

            orderList.DateMessage = DateTime.Now;

            return await Task.FromResult(orderList);
        }
    }
}

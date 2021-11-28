using Infrasctuture.Service.Interfaces;
using System.Collections.Generic;

namespace Infrasctuture.Service.Settings
{
    public class TopicSettings : ITopicSettings
    {
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }
        public List<Queue> Queues { get; set; }
    }
}

using Infrasctuture.Service.Interfaces;

namespace Infrasctuture.Service.Settings
{
    public class OrderTopic : ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }
}

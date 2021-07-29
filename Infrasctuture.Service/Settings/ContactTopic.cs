using Infrasctuture.Service.Interfaces;

namespace Infrasctuture.Service.Settings
{
    public class ContactTopic : ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }
}

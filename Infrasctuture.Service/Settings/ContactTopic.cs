using Infrasctuture.Service.Interfaces;

namespace Infrasctuture.Service.Settings
{
    public class ContactTopic : ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }
    }
}

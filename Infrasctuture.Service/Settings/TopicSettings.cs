using Infrasctuture.Service.Interfaces;

namespace Infrasctuture.Service.Settings
{
    public class TopicSettings : ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }
    }
}

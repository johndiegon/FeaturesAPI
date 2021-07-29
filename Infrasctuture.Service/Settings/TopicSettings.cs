using Infrasctuture.Service.Interfaces;

namespace Infrasctuture.Service.Settings
{
    public class TopicSettings : ITopicSettings
    {
        public string ConnectionString { get; set; }
    }
}

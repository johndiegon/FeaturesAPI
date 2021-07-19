namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }
}

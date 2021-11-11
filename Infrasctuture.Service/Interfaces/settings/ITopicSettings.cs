namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicSettings
    {
        public string ConnectionString { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }

    }
}

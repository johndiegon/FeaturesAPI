namespace Infrasctuture.Service.Interfaces.settings
{
    public interface IBlobSettings
    {
        public string ConnectionString { get; set; }
        public string BucketName { get; set; }
        public string BucketImageName { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }

    }
}

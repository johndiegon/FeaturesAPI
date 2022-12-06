using Infrasctuture.Service.Interfaces.settings;

namespace Infrasctuture.Service.Settings
{
    public class OrderBlob : IBlobSettings
    {
        public string ConnectionString { get; set; }
        public string BucketName { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }
        public string BucketImageName { get; set; }
    }
}

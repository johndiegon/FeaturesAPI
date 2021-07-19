using Infrasctuture.Service.Interfaces.settings;

namespace Infrasctuture.Service.Settings
{
    public class OrderBlob : IBlobSettings
    {
        public string ConnectionString { get; set; }
    }
}

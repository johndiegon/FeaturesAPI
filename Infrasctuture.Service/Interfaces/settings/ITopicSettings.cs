using Infrasctuture.Service.Settings;
using System.Collections.Generic;

namespace Infrasctuture.Service.Interfaces
{
    public interface ITopicSettings
    {
        public List<Queue> Queues { get; set; }
        public string IDAccessKey { get; set; }
        public string AccessKey { get; set; }

    }
}

using System;

namespace Infrasctuture.Service.Contracts
{
    public class ImportedFile
    {
        public string IdClient { get; set; }
        public string PathFile { get; set; }
        public string FileName { get; set; }
        public DateTime DateMessage { get; set; }

    }
}

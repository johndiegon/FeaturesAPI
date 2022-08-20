using Microsoft.AspNetCore.Http;
using Sentry;
using System;
using System.Threading.Tasks;

namespace Infrasctuture.Service.Interfaces
{
    public interface ILoggerCrossCutting
    {
        void SendErrorAsync(Exception exception, string scope);
        Task CaptureAsync(Exception exception);
        ITransaction GetTransactionAsync(HttpContext context);
    }
}

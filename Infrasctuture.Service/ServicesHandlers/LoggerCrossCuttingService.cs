using Infrasctuture.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Sentry;
using System;
using System.Threading.Tasks;

namespace Infrasctuture.Service.ServicesHandlers
{
    public class LoggerCrossCuttingService : ILoggerCrossCutting
    {


        public LoggerCrossCuttingService()
        {
        }


        public void SendErrorAsync(Exception exception, string scope)
        {
            SentrySdk.CaptureException(exception, _scope =>
            {
                _scope.SetTag("scope", scope);
                _scope.Environment = "Prod";
            });
        }

        public async Task CaptureAsync(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            SentrySdk.CaptureEvent(new SentryEvent(exception));
        }

        public ITransaction GetTransactionAsync(HttpContext context)
        {
            var transaction = SentrySdk.StartTransaction(context.Request.Path, context.Request.Method);
            transaction.Request.Url = context.Request.Path;
            transaction.Request.Method = context.Request.Method;
            transaction.Request.Data = context.Request.Body;

            return transaction;
        }

    }
}

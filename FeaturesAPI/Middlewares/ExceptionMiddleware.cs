using Domain.Models;
using Infrasctuture.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

namespace FeaturesAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILoggerCrossCutting _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext httpContext, ILoggerCrossCutting logger)
        {
            _logger = logger;
            var transaction = _logger.GetTransactionAsync(httpContext);

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _ = _logger.CaptureAsync(ex);

                await HandleExceptionAsync(httpContext, ex);
            }
            finally
            {
                transaction.Finish();
            }
        }

        private static System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new CommandResponse()
            {
                Notification = new List<Notification>(),
                Data = new Data
                {
                    Message = "Internal Error",
                    Status = Status.Error,
                }
            };
            response.Notification.Add(new Notification { Exception = exception.Message, Message = "Internal error" });
            return context.Response.WriteAsync(response.ToString());
        }
    }
}

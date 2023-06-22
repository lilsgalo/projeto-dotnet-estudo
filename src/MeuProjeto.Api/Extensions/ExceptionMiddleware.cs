using System;
using System.Net;
using System.Threading.Tasks;
using MeuProjeto.Business.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MeuProjeto.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLogger> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<CustomLogger> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex, _logger);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception, ILogger<CustomLogger> logger)
        {
            var message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
            logger.LogCritical("[{message}] [{type}] [{tablename}] [{oldvalues}] [{newvalues}]", message, (int)LogTypeEnum.Exception, "", exception.StackTrace, "");
            //exception.Ship(context);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
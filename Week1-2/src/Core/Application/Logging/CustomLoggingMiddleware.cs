using CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"[LoggingMiddleware]: Request: {context.Request.Path} [{context.Request.Method}]");
            await _next(context);
            _logger.LogInformation($"[LoggingMiddleware]: Request: {context.Request.Path} [{context.Request.Method}]. Status Code: {context.Response.StatusCode}");
        }
    }

    public static class CustomLoggingMiddlewareExtension
    {
        public static void UseLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomLoggingMiddleware>();
        }
    }
}

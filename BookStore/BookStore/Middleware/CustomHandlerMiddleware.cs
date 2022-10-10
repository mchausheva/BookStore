using Newtonsoft.Json;
using System.Net;

namespace BookStore.Middleware
{
    public class CustomHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomHandlerMiddleware> _logger;
        public CustomHandlerMiddleware(RequestDelegate next, ILogger<CustomHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogWarning(context.Request.Method);
                _logger.LogInformation(
                            "Request {method} => {statusCode}",
                            context.Request?.Method,
                            context.Response?.StatusCode);
            }
        }
    }
}

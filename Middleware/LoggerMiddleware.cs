using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace CSharpDefender.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var path = context.Request.Path;
            var method = context.Request.Method;
            Console.WriteLine($"Request: {method} {path} from {ip}");
            await _next(context);
            Console.WriteLine($"Response: {context.Response.StatusCode} for {method} {path} from {ip}");
        }
    }
} 
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CSharpDefender.Middleware
{
    public class ConnectionLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, int> Connections = new();
        private const int MAX_CONNECTIONS = 10;

        public ConnectionLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            Connections.AddOrUpdate(ip, 1, (_, count) => count + 1);
            if (Connections[ip] > MAX_CONNECTIONS)
            {
                Connections.AddOrUpdate(ip, 0, (_, count) => count - 1);
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Connection limit exceeded.");
                return;
            }
            try
            {
                await _next(context);
            }
            finally
            {
                Connections.AddOrUpdate(ip, 0, (_, count) => count > 0 ? count - 1 : 0);
            }
        }
    }
} 
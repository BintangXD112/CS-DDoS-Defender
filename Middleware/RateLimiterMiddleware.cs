using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;

namespace CSharpDefender.Middleware
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> Requests = new();
        private const int LIMIT = 100;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(10);

        public RateLimiterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;
            var (count, windowStart) = Requests.GetOrAdd(ip, _ => (0, now));
            if (now - windowStart > WINDOW)
            {
                Requests[ip] = (1, now);
            }
            else
            {
                if (count >= LIMIT)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Rate limit exceeded.");
                    return;
                }
                Requests[ip] = (count + 1, windowStart);
            }
            await _next(context);
        }
    }
} 
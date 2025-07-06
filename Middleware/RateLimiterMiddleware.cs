using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;
using Microsoft.Extensions.Configuration;

namespace CSharpDefender.Middleware
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> Requests = new();
        private readonly int _limit;
        private readonly TimeSpan _window;

        public RateLimiterMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var section = configuration.GetSection("AntiDDoS:RateLimiter");
            _limit = section.GetValue<int>("Limit", 100);
            _window = TimeSpan.FromMinutes(section.GetValue<int>("WindowMinutes", 10));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            RateLimiterStats.TotalRequests++;
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;
            var (count, windowStart) = Requests.GetOrAdd(ip, _ => (0, now));
            if (now - windowStart > _window)
            {
                Requests[ip] = (1, now);
            }
            else
            {
                if (count >= _limit)
                {
                    RateLimiterStats.Triggered++;
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
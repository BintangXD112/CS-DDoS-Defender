using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;

namespace CSharpDefender.Middleware
{
    public class TokenLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> TokenRequests = new();
        private const int LIMIT = 50;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(10);

        public TokenLimiterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                var now = DateTime.UtcNow;
                var (count, windowStart) = TokenRequests.GetOrAdd(token, _ => (0, now));
                if (now - windowStart > WINDOW)
                {
                    TokenRequests[token] = (1, now);
                }
                else
                {
                    if (count >= LIMIT)
                    {
                        context.Response.StatusCode = 429;
                        await context.Response.WriteAsync("Token rate limit exceeded.");
                        return;
                    }
                    TokenRequests[token] = (count + 1, windowStart);
                }
            }
            await _next(context);
        }
    }
} 
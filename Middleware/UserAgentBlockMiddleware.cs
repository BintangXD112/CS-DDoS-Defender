using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpDefender.Middleware
{
    public class UserAgentBlockMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly HashSet<string> BlockedAgents = new() { "curl", "python", "httpclient" };

        public UserAgentBlockMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower();
            foreach (var blocked in BlockedAgents)
            {
                if (userAgent.Contains(blocked))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("User-Agent blocked.");
                    return;
                }
            }
            await _next(context);
        }
    }
} 
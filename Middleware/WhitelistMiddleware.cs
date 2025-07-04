using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpDefender.Middleware
{
    public class WhitelistMiddleware
    {
        private readonly RequestDelegate _next;
        // Simulasi whitelist IP (in-memory)
        private static HashSet<string> WhitelistedIps = new HashSet<string>();

        public WhitelistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (WhitelistedIps.Count > 0 && (ip == null || !WhitelistedIps.Contains(ip)))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Your IP is not whitelisted.");
                return;
            }
            await _next(context);
        }
    }
} 
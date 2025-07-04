using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpDefender.Middleware
{
    public class BlocklistMiddleware
    {
        private readonly RequestDelegate _next;
        // Simulasi blocklist IP (in-memory, sama dengan controller)
        private static HashSet<string> BlockedIps = Controllers.BlocklistController.BlockedIps;

        public BlocklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (ip != null && BlockedIps.Contains(ip))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Your IP is blocked.");
                return;
            }
            await _next(context);
        }
    }
} 
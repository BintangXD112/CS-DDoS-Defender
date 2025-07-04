using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpDefender.Middleware
{
    public class PathBlockerMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly HashSet<string> BlockedPaths = new() { "/admin", "/config" };

        public PathBlockerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();
            if (BlockedPaths.Contains(path))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Path blocked.");
                return;
            }
            await _next(context);
        }
    }
} 
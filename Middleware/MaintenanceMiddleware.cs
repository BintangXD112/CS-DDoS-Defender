using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSharpDefender.Middleware
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        public static bool IsMaintenance = false;

        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsMaintenance)
            {
                context.Response.StatusCode = 503;
                await context.Response.WriteAsync("Maintenance mode active.");
                return;
            }
            await _next(context);
        }
    }
} 
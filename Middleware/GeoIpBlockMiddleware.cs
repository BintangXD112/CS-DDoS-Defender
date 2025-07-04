using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CSharpDefender.Controllers;

namespace CSharpDefender.Middleware
{
    public class GeoIpBlockMiddleware
    {
        private readonly RequestDelegate _next;

        public GeoIpBlockMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Dummy: asumsikan IP "1.1.1.1" dari AU, "8.8.8.8" dari US
            var ip = context.Connection.RemoteIpAddress?.ToString();
            string country = ip switch
            {
                "1.1.1.1" => "AU",
                "8.8.8.8" => "US",
                _ => null
            };
            if (country != null && GeoIpController.BlockedCountries.Contains(country))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"Access from {country} is blocked.");
                return;
            }
            await _next(context);
        }
    }
} 
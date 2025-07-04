using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CSharpDefender.Middleware
{
    public class PatternAnalysisMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Regex BlockedPattern = new Regex(@"/api/.*admin.*", RegexOptions.IgnoreCase);

        public PatternAnalysisMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (BlockedPattern.IsMatch(path))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Pattern blocked.");
                return;
            }
            await _next(context);
        }
    }
} 
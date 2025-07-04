using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSharpDefender.Middleware
{
    public class JsChallengeMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CookieName = "js_challenge_passed";

        public JsChallengeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey(CookieName) && context.Request.Path != "/js-challenge")
            {
                context.Response.Redirect("/js-challenge");
                return;
            }
            await _next(context);
        }
    }
} 
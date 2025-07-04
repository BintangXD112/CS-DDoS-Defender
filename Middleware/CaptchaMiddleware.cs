using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSharpDefender.Middleware
{
    public class CaptchaMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CookieName = "captcha_passed";

        public CaptchaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey(CookieName) && context.Request.Path != "/captcha")
            {
                context.Response.Redirect("/captcha");
                return;
            }
            await _next(context);
        }
    }
} 
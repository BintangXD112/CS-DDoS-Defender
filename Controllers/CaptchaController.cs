using Microsoft.AspNetCore.Mvc;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptchaController : ControllerBase
    {
        [HttpGet]
        [Route("/captcha")]
        public ContentResult GetCaptcha()
        {
            var html = @"<html><body><form method='post'><input name='answer' placeholder='Tulis 1234'/><button type='submit'>Submit</button></form></body></html>";
            return new ContentResult { Content = html, ContentType = "text/html" };
        }

        [HttpPost]
        [Route("/captcha")]
        public IActionResult PostCaptcha([FromForm] string answer)
        {
            if (answer == "1234")
            {
                Response.Cookies.Append("captcha_passed", "1");
                return Redirect("/");
            }
            return Content("Captcha salah. <a href='/captcha'>Coba lagi</a>", "text/html");
        }
    }
} 
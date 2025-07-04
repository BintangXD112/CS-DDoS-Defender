using Microsoft.AspNetCore.Mvc;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsChallengeController : ControllerBase
    {
        [HttpGet]
        [Route("/js-challenge")]
        public ContentResult GetChallenge()
        {
            var html = @"<html><body><script>document.cookie='js_challenge_passed=1;path=/';window.location='/'</script><noscript>Enable JavaScript!</noscript></body></html>";
            return new ContentResult { Content = html, ContentType = "text/html" };
        }
    }
} 
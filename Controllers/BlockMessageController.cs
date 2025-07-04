using Microsoft.AspNetCore.Mvc;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockMessageController : ControllerBase
    {
        [HttpGet("/blocked")]
        public ContentResult Blocked()
        {
            var html = @"<html><body><h1>Access Blocked</h1><p>Your access has been blocked.</p></body></html>";
            return new ContentResult { Content = html, ContentType = "text/html" };
        }

        [HttpGet("/permanent_blocked")]
        public ContentResult PermanentBlocked()
        {
            var html = @"<html><body><h1>Permanently Blocked</h1><p>Your access has been permanently blocked.</p></body></html>";
            return new ContentResult { Content = html, ContentType = "text/html" };
        }
    }
} 
using Microsoft.AspNetCore.Mvc;
using System;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveWebhook([FromBody] object payload)
        {
            Console.WriteLine($"Webhook received: {payload}");
            return Ok(new { message = "Webhook received" });
        }
    }
} 
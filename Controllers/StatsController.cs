using CSharpDefender.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStats()
        {
            return Ok(new
            {
                blocklistCount = BlocklistController.BlockedIps.Count,
                whitelistCount = WhitelistController.WhitelistedIps.Count,
                rateLimitTriggered = RateLimiterMiddleware.RateLimitTriggeredCount
            });
        }
    }
} 
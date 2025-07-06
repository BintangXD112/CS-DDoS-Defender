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
            // Statistik tambahan (dummy, bisa dihubungkan ke middleware sebenarnya)
            int rateLimitTriggered = RateLimiterStats.Triggered;
            int totalRequests = RateLimiterStats.TotalRequests;
            return Ok(new
            {
                blocklistCount = BlocklistController.BlockedIps.Count,
                whitelistCount = WhitelistController.WhitelistedIps.Count,
                rateLimitTriggered,
                totalRequests
            });
        }
    }

    // Tambahkan class statis sederhana untuk simulasi statistik
    public static class RateLimiterStats
    {
        public static int Triggered = 0;
        public static int TotalRequests = 0;
    }
} 
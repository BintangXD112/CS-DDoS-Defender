using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlocklistController : ControllerBase
    {
        // Simulasi blocklist IP (in-memory)
        private static HashSet<string> BlockedIps = new HashSet<string>();

        [HttpGet]
        public IActionResult GetBlockedIps()
        {
            return Ok(BlockedIps);
        }

        [HttpPost]
        public IActionResult AddBlockedIp([FromBody] string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return BadRequest("IP tidak valid");
            BlockedIps.Add(ip);
            return Ok(new { message = $"IP {ip} diblokir." });
        }
    }
} 
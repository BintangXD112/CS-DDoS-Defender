using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhitelistController : ControllerBase
    {
        // Simulasi whitelist IP (in-memory)
        public static HashSet<string> WhitelistedIps = new HashSet<string>();

        [HttpGet]
        public IActionResult GetWhitelistedIps()
        {
            return Ok(WhitelistedIps);
        }

        [HttpPost]
        public IActionResult AddWhitelistedIp([FromBody] string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return BadRequest("IP tidak valid");
            WhitelistedIps.Add(ip);
            return Ok(new { message = $"IP {ip} di-whitelist." });
        }
    }
} 
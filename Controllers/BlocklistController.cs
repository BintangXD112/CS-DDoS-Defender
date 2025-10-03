using CSharpDefender.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlocklistController : ControllerBase
    {
        // Simulasi blocklist IP (in-memory)
        private static readonly HashSet<string> BlockedIps = new HashSet<string>();

        [HttpGet]
        public IActionResult GetBlockedIps()
        {
            return Ok(BlockedIps);
        }

        [HttpPost]
        public IActionResult AddBlockedIp([FromBody] IpModel model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.Ip))
                return BadRequest("IP tidak valid");
            BlockedIps.Add(model.Ip);
            return Ok(new { message = $"IP {model.Ip} diblokir." });
        }
    }
} 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoIpController : ControllerBase
    {
        internal static HashSet<string> BlockedCountries = new HashSet<string>();

        [HttpGet]
        public IActionResult GetBlockedCountries()
        {
            return Ok(BlockedCountries);
        }

        [HttpPost]
        public IActionResult AddBlockedCountry([FromBody] string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return BadRequest("Kode negara tidak valid");
            BlockedCountries.Add(countryCode.ToUpper());
            return Ok(new { message = $"Negara {countryCode} diblokir." });
        }
    }
} 
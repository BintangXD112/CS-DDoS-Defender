using Microsoft.AspNetCore.Mvc;
using CSharpDefender.Middleware;

namespace CSharpDefender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        [HttpPost("on")]
        public IActionResult EnableMaintenance()
        {
            MaintenanceMiddleware.IsMaintenance = true;
            return Ok(new { message = "Maintenance mode enabled." });
        }

        [HttpPost("off")]
        public IActionResult DisableMaintenance()
        {
            MaintenanceMiddleware.IsMaintenance = false;
            return Ok(new { message = "Maintenance mode disabled." });
        }
    }
} 
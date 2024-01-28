using Asp.Versioning;
using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HealthController : ControllerBase
    {
        private static string CLASS_NAME = "HealthController";
        ICustomLogger _logger;
        public HealthController(ICustomLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "USER")]
        public ActionResult ApiHealthCheck()
        {
            var ip =  HttpContext.Connection.RemoteIpAddress;
            _logger.LogInfo(CLASS_NAME, "ApiHealthCheck", "API is Healthy");
            return Ok("API is Healthy");
        }
    }
}

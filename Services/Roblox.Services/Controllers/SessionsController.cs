using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Sessions/")]
    public class SessionsController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetServiceStatus()
        {
            return new()
            {
                name = "Roblox.Services.Sessions",
                status = "OK"
            };
        }
    }
}
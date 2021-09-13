using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Users/")]
    public class UsersController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetServiceStatus()
        {
            return new()
            {
                name = "Roblox.Services.Users",
                status = "OK"
            };
        }
    }
}
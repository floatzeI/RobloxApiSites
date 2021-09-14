using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Avatar")]
    public class AvatarController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetStatus()
        {
            return new()
            {
                name = "Roblox.Services.Avatar",
                status = "OK"
            };
        }
    }
}
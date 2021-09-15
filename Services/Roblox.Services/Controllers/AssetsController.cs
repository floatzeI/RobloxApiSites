using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Assets")]
    public class AssetsController : ControllerBase
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetStatus()
        {
            return new()
            {
                name = "Roblox.Assets.Service",
                status = "OK"
            };
        }
    }
}
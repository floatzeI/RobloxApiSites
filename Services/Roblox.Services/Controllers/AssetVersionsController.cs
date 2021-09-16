using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/AssetVersions")]
    public class AssetVersionsController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetStatus()
        {
            return new()
            {
                name = "Roblox.AssetVersions.Storage",
                status = "OK"
            };
        }
    }
}
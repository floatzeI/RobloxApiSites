using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Ownership")]
    public class OwnershipController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetStatus()
        {
            return new()
            {
                name = "Roblox.Ownership.Service",
                status = "OK"
            };
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Passwords")]
    public class PasswordsController
    {
        [HttpGet]
        public Models.Shared.HealthCheckResponse GetStatus()
        {
            return new()
            {
                name = "Roblox.Passwords.Service",
                status = "OK"
            };
        }
    }
}
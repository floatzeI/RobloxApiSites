using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public async Task Login()
        {
            throw new NotImplementedException();
        }
    }
}
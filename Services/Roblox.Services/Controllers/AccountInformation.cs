using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/User/v1")]
    public class AccountInformationController
    {
        [HttpGet("GetUserDescription")]
        public async Task<string> GetUserDescription(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
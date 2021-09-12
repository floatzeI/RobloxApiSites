using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Users/v1")]
    public class UsersController
    {
        private IUsersService usersService { get; }
        public UsersController(Services.IUsersService service)
        {
            usersService = service;
        }
        
        [HttpGet("GetUserDescription")]
        public async Task<Models.Users.UserDescriptionEntry> GetUserDescription(long userId)
        {
            return  await usersService.GetDescription(userId);
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Users/v1")]
    public class AccountInformationController
    {
        private IAccountInformationService accountInformationService { get; }
        public AccountInformationController(Services.IAccountInformationService service)
        {
            accountInformationService = service;
        }
        
        [HttpGet("GetUserDescription")]
        public async Task<Models.AccountInformation.AccountDescriptionEntry> GetUserDescription(long userId)
        {
            return  await accountInformationService.GetDescription(userId);
        }
    }
}
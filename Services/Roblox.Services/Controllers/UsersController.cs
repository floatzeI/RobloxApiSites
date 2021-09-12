using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Models;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Users/v1")]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public class UsersController
    {
        private IUsersService usersService { get; }
        public UsersController(Services.IUsersService service)
        {
            usersService = service;
        }
        
        /// <summary>
        /// Get the description of an account by its ID
        /// </summary>
        /// <param name="userId">The account to get a description for</param>
        /// <response code="400">
        /// RecordNotFound: Could not find a record for an account with the userId
        /// </response>
        [HttpGet("GetUserDescription")]
        public async Task<Models.Users.UserDescriptionEntry> GetUserDescription([Required] long userId)
        {
            return  await usersService.GetDescription(userId);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Exceptions.Services;
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
        /// Get information for an account from their userId
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("GetUserInformation")]
        public async Task<Models.Users.UserInformationResponse> GetUserById(long userId)
        {
            return await usersService.GetUserById(userId);
        }

        /// <summary>
        /// Create a new account and account_information
        /// </summary>
        /// <returns>The created user's details</returns>
        [HttpPost("CreateUser")]
        public async Task<Models.Users.UserAccountEntry> CreateUser([Required] Models.Users.CreateUserRequest request)
        {
            var staticValidationError = request.Validate();
            if (staticValidationError != null)
            {
                throw new ParameterException(staticValidationError);
            }
            var birth = usersService.GetDateTimeFromBirthDate(request.birthYear, request.birthMonth, request.birthDay);
            var user = await usersService.CreateUser(request.username);
            await usersService.SetUserBirthDate(user.userId, birth);
            return user;
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
            return await usersService.GetDescription(userId);
        }

        /// <summary>
        /// Insert or update the description for the provided account ID
        /// </summary>
        /// <param name="request">The description request</param>
        [HttpPost("SetUserDescription")]
        public async Task SetUserDescription([Required, FromBody] Models.Users.SetDescriptionRequest request)
        {
            await usersService.SetUserDescription(request.userId, request.description);
        }
    }
}
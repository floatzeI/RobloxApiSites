using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
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
        /// Delete a user account by its ID. The account must be less than 10 minutes old.
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="ParameterException"></exception>
        [HttpPost("DeleteUser")]
        public async Task DeleteUser([Required] long userId)
        {
            var userInfo = await usersService.GetUserById(userId);
            if (userInfo.created > DateTime.Now.Subtract(TimeSpan.FromMinutes(10)))
            {
                await usersService.DeleteUser(userId);
            }
            else
            {
                throw new ParameterException("userId");
            }
        }

        /// <summary>
        /// Set the user's birth date
        /// </summary>
        /// <response code="400">
        /// Birth date is not valid. It must be less than 100 years ago, and correspond to an actual date.
        /// </response>
        [HttpPost("SetUserBirthDate")]
        public async Task SetUserBirthDate([Required] Models.Users.SetBirthDateRequest request)
        {
            var error = request.Validate();
            if (error != null)
            {
                throw new ParameterException(error);
            }
            var date = usersService.GetDateTimeFromBirthDate(request.birthYear, request.birthMonth, request.birthDay);
            await usersService.SetUserBirthDate(request.userId, date);
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
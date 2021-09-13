using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Users.Client;
using Roblox.Web.WebAPI;
using Roblox.Web.WebAPI.Controllers;

namespace Roblox.AccountInformation.Api.Controllers.V1 {
    /// <summary>Account Information Api v1</summary>
    [ApiController]
    [Route("/v1/")]
    public class AccountInformationController : ApiControllerBase
    {
        private IUsersV1Client usersV1Client { get; }
        public AccountInformationController(IUsersV1Client usersV1Client)
        {
            this.usersV1Client = usersV1Client;
        }
        /// <summary>Get the user's birthdate</summary>
        [HttpGet("birthdate")]
        public async Task<AccountInformation.Api.Models.BirthdayResponse> GetBirthdate()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>Update the user's birthdate</summary>
        [HttpPost("birthdate")]
        public async Task<Web.WebAPI.ApiEmptyResponseModel> SetBirthdate(AccountInformation.Api.Models.BirthdayRequest request)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>Get the user's description</summary>
        [HttpGet("description")]
        [LoggedIn]
        public async Task<AccountInformation.Api.Models.DescriptionResponse> GetDescription()
        {
            var result = await usersV1Client.GetDescription(authenticatedUser.id);
            return new()
            {
                description = result.description,
            };
        }
        
        /// <summary>
        /// Update the user's description
        /// </summary>
        /// <response code="400">
        /// 1: User Not Found
        /// </response>
        /// <response code="401">
        /// 0: Authorization has been denied for this request.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// 2: PIN is locked.
        /// </response>
        /// <response code="503">
        /// 3: This feature is currently disabled. Please try again later.
        /// </response>
        [HttpPost("description")]
        [LoggedIn]
        public async Task<AccountInformation.Api.Models.DescriptionResponse> SetDescription(AccountInformation.Api.Models.DescriptionRequest request)
        {
            var newDescription = request.description;
            if (newDescription != null)
            {
                if (newDescription.Length > 999)
                {
                    newDescription = newDescription.Substring(0, 999);
                }   
                
                if (newDescription.Trim() == "")
                {
                    newDescription = null;
                }
            }

            await usersV1Client.SetDescription(authenticatedUser.id, newDescription);
            return new()
            {
                description = newDescription
            };
        }
        
        /// <summary>Get the user's gender</summary>
        [HttpGet("gender")]
        public async Task<AccountInformation.Api.Models.GenderResponse> GetGender()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>Update the user's gender</summary>
        [HttpPost("gender")]
        public async Task<Web.WebAPI.ApiEmptyResponseModel> SetGender(AccountInformation.Api.Models.GenderRequest request)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>Returns number of consecutive login days for xbox users</summary>
        [HttpGet("xbox-live/consecutive-login-days")]
        public async Task<AccountInformation.Api.Models.ConsecutiveLoginDaysResponse> GetXboxLiveConsecutiveLoginDays()
        {
            throw new NotImplementedException();
        }
    }
}
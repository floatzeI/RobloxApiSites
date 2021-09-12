using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Users.Client;

namespace Roblox.AccountInformation.Api.Controllers.V1 {
    /// <summary>Account Information Api v1</summary>
    [ApiController]
    [Route("/v1/")]
    public class AccountInformationController
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
        public async Task<AccountInformation.Api.Models.DescriptionResponse> GetDescription()
        {
            var result = await usersV1Client.GetDescription(1);
            return new()
            {
                description = result.description,
            };
        }
        
        /// <summary>Update the user's description</summary>
        [HttpPost("description")]
        public async Task<AccountInformation.Api.Models.DescriptionResponse> SetDescription(AccountInformation.Api.Models.DescriptionRequest request)
        {
            throw new NotImplementedException();
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
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Authentication.Api.Models;
using Roblox.Authentication.Api.Validators;
using Roblox.Passwords.Client;
using Roblox.Web.Authentication.Passwords;
using Roblox.Web.WebAPI;
using Roblox.Web.WebAPI.Controllers;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/")]
    public class PasswordsController : ApiControllerBase
    {
        private IPasswordsV1Client passwordsClient { get; set; }
        public PasswordsController(IPasswordsV1Client passwordsClient)
        {
            this.passwordsClient = passwordsClient;
        }
        
        /// <summary>
        /// Endpoint for checking if a password is valid.
        /// </summary>
        /// <response code="400">
        /// 1: Valid Username and Password are required. Please try again.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// </response>
        [HttpGet("passwords/validate")]
        public Models.PasswordValidationResponse ValidateFromUri([Required, FromQuery] Models.PasswordValidationRequest request)
        {
            return Validators.PasswordValidator.GetPasswordStatus(request.username, request.password);
        }
        
        /// <summary>
        /// Endpoint for checking if a password is valid.
        /// </summary>
        /// <response code="400">
        /// 1: Valid Username and Password are required. Please try again.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// </response>
        [HttpPost("passwords/validate")]
        public Models.PasswordValidationResponse ValidateFromBody([Required, FromBody] Models.PasswordValidationRequest request)
        {
            return Validators.PasswordValidator.GetPasswordStatus(request.username, request.password);
        }

        /// <summary>
        /// Changes the password for the authenticated user.
        /// </summary>
        /// <remarks>
        /// The current password is needed for verification that the password can be changed.
        /// </remarks>
        /// <param name="request">The request model including the current password, and the new password.</param>
        /// <response code="200">
        /// Password successfully changed.
        /// </response>
        /// <response code="400">
        /// {<see cref="Roblox.Web.Authentication.Passwords.PasswordResponseCodes.InvalidCurrentPassword"/>}
        /// OR
        /// {<see cref="Roblox.Web.Authentication.Passwords.PasswordResponseCodes.InvalidPassword"/>}
        /// </response>
        /// <response code="401">
        /// 0: Authorization has been denied for this request.
        /// </response>
        /// <response code="403">
        /// {<see cref="Roblox.Web.Authentication.Passwords.PasswordResponseCodes.PinLocked"/>}
        /// 0: Token Validation Failed
        /// </response>
        /// <response code="429">
        /// {<see cref="Roblox.Web.Authentication.Passwords.PasswordResponseCodes.Flooded"/>}
        /// </response>
        [HttpPost("user/passwords/change")]
        [LoggedIn]
        public async Task<ApiEmptyResponseModel> ChangePassword([Required, FromBody] Models.PasswordChangeModel request)
        {
            // confirm it is matching
            var isCurrentValid = await passwordsClient.IsPasswordCorrect(authenticatedUser.id, request.currentPassword);
            if (isCurrentValid != true)
            {
                throw new BadRequestException(PasswordResponseCodes.InvalidCurrentPassword, "Current password is invalid");
            }
            // confirm new password fulfills rules
            var newPasswordValid = PasswordValidator.GetPasswordStatus(authenticatedUser.name, request.newPassword);
            if (newPasswordValid.code != PasswordValidationStatus.ValidPassword)
            {
                throw new BadRequestException(PasswordResponseCodes.InvalidPassword, "New password is invalid");
            }
            // change the password
            await passwordsClient.SetPassword(authenticatedUser.id, request.newPassword);
            // todo: invalidate all sessions aside from one changing password, send email informing user password was changed

            return new();
        }
    }
}
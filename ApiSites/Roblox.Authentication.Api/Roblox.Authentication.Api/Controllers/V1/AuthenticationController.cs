using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roblox.Authentication.Api.Exceptions;
using Roblox.Passwords.Client;
using Roblox.Passwords.Client.Exceptions;
using Roblox.Platform.Authentication;
using Roblox.Platform.Membership;
using Roblox.Sessions.Client;
using Roblox.Users.Client;
using Roblox.Users.Client.Exceptions;
using Roblox.Web.WebAPI;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/")]
    public class AuthenticationController : ControllerBase
    {
        private IUsersV1Client usersClient { get; set; }
        private IPasswordsV1Client passwordsClient { get; set; }
        private ISessionsV1Client sessionsClient { get; set; }

        public AuthenticationController(IUsersV1Client usersClient, IPasswordsV1Client passwordsClient, ISessionsV1Client sessionsClient)
        {
            this.usersClient = usersClient;
            this.passwordsClient = passwordsClient;
            this.sessionsClient = sessionsClient;
        }

        /// <summary>
        /// Gets Auth meta data
        /// </summary>
        [HttpGet("auth/metadata")]
        public Models.AuthMetaDataResponse GetMetaData()
        {
            return new();
        }
        
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <response code="400">
        /// 0: An unexpected error occurred.
        /// 3: Username and Password are required. Please try again.
        /// 8: Login with received credential type is not supported.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// 1: Incorrect username or password. Please try again.
        /// 2: You must pass the robot test before logging in.
        /// 4: Account has been locked. Please request a password reset.
        /// 5: Unable to login. Please use Social Network sign on.
        /// 6: Account issue. Please contact Support.
        /// 9: Unable to login with provided credentials. Default login is required.
        /// 10: Received credentials are unverified.
        /// 12: Existing login session found. Please log out first.
        /// 14: The account is unable to log in. Please log in to the LuoBu app.
        /// 15: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="429">
        /// 7: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="503">
        /// 11: Service unavailable. Please try again.
        /// </response>
        [HttpPost("login")]
        public async Task<Models.LoginResponse> Login([Required, FromBody] Models.LoginRequest request)
        {
            // temporary until other methods are added...
            if (request.ctype != CredentialsType.Username)
            {
                throw new CredentialsNotSuitableForLogin();
            }
            // convert username to userId
            Users.Client.Models.Responses.SkinnyUserEntry userInfo;
            try
            {
                userInfo = await usersClient.GetUserByUsername(request.cvalue);
            }
            catch (UserNotFoundException)
            {
                throw new IncorrectCredentialsException();
            }
            // check if password is correct
            try
            {
                var passwordCorrect = await passwordsClient.IsPasswordCorrect(userInfo.userId, request.password);
                if (passwordCorrect != true)
                {
                    throw new IncorrectCredentialsException();
                }
            }
            catch (UserHasNoPasswordException)
            {
                throw new LoginAccountIssueException();
            }
            // password is correct... get extended details
            var extendedUserDetails = await usersClient.GetUserById(userInfo.userId);
            if (extendedUserDetails.accountStatus == AccountStatus.Forgotten)
            {
                throw new IncorrectCredentialsException();
            }
            // check if account is locked
            if (extendedUserDetails.accountStatus == AccountStatus.MustValidateEmail)
            {
                throw new LockedAccountException();
            }
            // create a session
            var userSession = await sessionsClient.CreateSession(userInfo.userId);
            // set cookie
            var expiration = DateTime.Now.AddYears(10);
            HttpContext.Response.Cookies.Append(".ROBLOSECURITY", userSession, new CookieOptions()
            {
                HttpOnly = true,
                Expires = new DateTimeOffset(expiration),
                Path = "/",
                IsEssential = true,
                // TODO: get domain from appsettings or something?
                // Domain = "roblox.com",
                // Secure = true, // roblox doesn't actually set the secure flag, probably for backwards compatability
            });
            // return result
            return new()
            {
                user = new()
                {
                    id = extendedUserDetails.userId,
                    name = extendedUserDetails.username,
                    displayName = extendedUserDetails.displayName,
                },
                twoStepVerificationData = null,
                identityVerificationLoginTicket = null,
                isBanned = extendedUserDetails.accountStatus != AccountStatus.Ok,
            };
        }

        /// <summary>
        /// Destroys the current authentication session.
        /// </summary>
        [HttpPost("logout")]
        [LoggedIn]
        public async Task<ApiEmptyResponseModel> LogOut()
        {
            var cookieValue = HttpContext.Request.Cookies[".ROBLOSECURITY"];
            await sessionsClient.DeleteSession(cookieValue);
            HttpContext.Response.Cookies.Append(".ROBLOSECURITY", "", new CookieOptions()
            {
                HttpOnly = true,
                Path = "/",
                IsEssential = true,
                Expires = new DateTimeOffset(DateTime.Now.Add(TimeSpan.FromSeconds(3))),
            });
            return new();
        }
    }
}
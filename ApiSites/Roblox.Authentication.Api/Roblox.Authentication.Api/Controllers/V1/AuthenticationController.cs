using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/")]
    public class AuthenticationController : ControllerBase
    {
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
        public async Task Login([Required, FromBody] Models.LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Authentication.Api.Models;
using Roblox.Authentication.Api.Validators;
using Roblox.Passwords.Client;
using Roblox.Sessions.Client;
using Roblox.Users.Client;
using Roblox.Users.Client.Exceptions;
using Roblox.Web.Authentication.Signup;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/signup")]
    public class SignupController
    {
        private IUsersV1Client usersClient { get; set; }
        private IPasswordsV1Client passwordsClient { get; set; }
        private ISessionsV1Client sessionsClient { get; set; }

        public SignupController(IUsersV1Client users, IPasswordsV1Client passwords, ISessionsV1Client sessions)
        {
            usersClient = users;
            passwordsClient = passwords;
            sessionsClient = sessions;
        }
        
        /// <summary>
        /// Endpoint for signing up a new user
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">
        /// Successful signup
        /// </response>
        /// <response code="400">
        /// Bad request
        /// 16: User agreement ids are null.
        /// </response>
        /// <response code="403">
        /// 0: Token Validation Failed
        /// 2: Captcha Failed.
        /// 4: Invalid Birthday.
        /// 5: Invalid Username.
        /// 6: Username already taken.
        /// 7: Invalid Password.
        /// 8: Password and Username are same.
        /// 9: Password is too simple.
        /// 10: Email is invalid.
        /// 11: Asset is invalid.
        /// 12: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="429">
        /// 3: Too many attempts. Please wait a bit.
        /// </response>
        /// <response code="500">
        /// Internal server error
        /// 15: Insert acceptances failed.
        /// </response>
        /// <response code="503">
        /// Service unavailable
        /// </response>
        [HttpPost]
        public async Task<Models.SignupResponse> Signup(Models.SignupRequest request)
        {
            // todo: what has to be called:
            // 1. Create user with UsersService
            // 2. insert gender
            // 3. Insert locale info for that user
            // 4. Insert password for that user
            // 5. If email specified, insert email record
            // 6. Create avatar (go off params if specified, otherwise use default specified in AppSettings)
            // 7. Add default avatar items to inventory, plus items specified in request.assetIds (as long as they're free items)
            // 8. add thumbnail and headshot
            // 9. Create default place and universe for the user
            // 10. Finally, Create session and set cookie
            
            // In the future, we may also want to look into captcha verification (mostly for realism)
            
            // implementation start
            try
            {
                await usersClient.GetUserByUsername(request.username);
                throw new ForbiddenException(SignupResponseCodes.UsernameTaken, "Username already taken.");
            }
            catch (UserNotFoundException)
            {
                // Good
            }

            DateTime birthDate;
            try
            {
                birthDate = DateTime.Parse(request.birthday);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ForbiddenException(SignupResponseCodes.InvalidBirthDay, "Invalid Birthday.");
            }

            var birthdayErrors = BirthdayValidator.ValidateBirthday(birthDate);
            if (birthdayErrors != null)
            {
                throw new ForbiddenException(SignupResponseCodes.InvalidBirthDay, "Invalid Birthday.");
            }

            var passwordErrors = PasswordValidator.GetPasswordStatus(request.username, request.password);
            if (passwordErrors.code != PasswordValidationStatus.ValidPassword)
            {
                throw new ForbiddenException(SignupResponseCodes.PasswordTooSimple, "Password is too simple.");
            }

            var user = await usersClient.InsertUser(request.username, birthDate.Year, birthDate.Month, birthDate.Day);
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                // capture, then rollback
                await usersClient.DeleteUser(user.userId);

                throw new Exception("Signup Failed", e);
            }
            throw new NotImplementedException();
        }
    }
}
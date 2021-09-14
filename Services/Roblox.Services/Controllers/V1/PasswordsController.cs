using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Passwords/v1/")]
    public class PasswordsController
    {
        private IPasswordsService service { get; set; }
        public PasswordsController(IPasswordsService passwordsService)
        {
            service = passwordsService;
        }
        
        /// <summary>
        /// Check if the provided password matches the password stored for the accountId
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>isCorrect will be true if the passwords match, otherwise it will be false</returns>
        /// <response code="400">
        /// RecordNotFound: A password entry for this user does not exist
        /// </response>
        [HttpPost("IsPasswordValid")]
        public async Task<Models.Passwords.ValidatePasswordResponse> IsPasswordValid([Required] Models.Passwords.ValidatePasswordRequest request)
        {
            var status = await service.IsPasswordCorrectForUser(request.userId, request.password);
            return new()
            {
                isCorrect = status,
            };
        }

        /// <summary>
        /// Insert or update the password for the user.
        /// </summary>
        /// <param name="request">The update request</param>
        [HttpPost("SetUserPassword")]
        public async Task SetUserPassword([Required] Models.Passwords.SetPasswordRequest request)
        {
            await service.SetPasswordForUser(request.userId, request.password);
        }
    }
}
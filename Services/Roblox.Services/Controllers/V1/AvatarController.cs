using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Avatar/v1")]
    public class AvatarController
    {
        private IAvatarService avatarService { get; set; }

        public AvatarController(IAvatarService service)
        {
            avatarService = service;
        }
        
        /// <summary>
        /// Create or set the avatar for the userId.
        /// </summary>
        [HttpPost("SetUserAvatar")]
        public async Task SetUserAvatar([Required] Models.Avatar.SetAvatarRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the avatar for the userId
        /// </summary>
        /// <param name="userId">The user to get the avatar for</param>
        /// <response code="400">
        /// RecordNotFound: User does not have an avatar
        /// </response>
        [HttpGet("GetUserAvatar")]
        public async Task<Models.Avatar.AvatarEntry> GetUserAvatar([Required] long userId)
        {
            return await avatarService.GetUserAvatar(userId);
        }
    }
}
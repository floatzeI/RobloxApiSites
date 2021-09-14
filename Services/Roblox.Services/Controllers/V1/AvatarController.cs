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
    }
}
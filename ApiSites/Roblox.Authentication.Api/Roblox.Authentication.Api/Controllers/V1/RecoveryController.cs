using Microsoft.AspNetCore.Mvc;

namespace Roblox.Authentication.Api.Controllers
{
    [ApiController]
    [Route("/v1/recovery")]
    public class RecoveryController
    {
        /// <summary>
        /// Get metadata for forgot endpoints
        /// </summary>
        /// <response code="503">
        /// 7: The Roblox WeChat API is currently unavailable.
        /// </response>
        [HttpGet("metadata")]
        public Models.RecoveryMetadataResponse GetMetadata()
        {
            return new()
            {
                isOnPhone = false,
                isPhoneFeatureEnabledForPassword = true,
                isPhoneFeatureEnabledForUsername = true,
                isBedev2CaptchaEnabledForPasswordReset = true,
                codeLength = 6,
            };
        }
    }
}
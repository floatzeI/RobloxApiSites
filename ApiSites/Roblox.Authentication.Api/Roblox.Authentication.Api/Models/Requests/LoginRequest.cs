using Roblox.Platform.Authentication;

namespace Roblox.Authentication.Api.Models
{
    public class LoginRequest
    {
        /// <summary>
        /// Credentials type <see cref="CredentialsType"/>
        /// </summary>
        public CredentialsType ctype { get; set; }   
        /// <summary>
        /// Credentials value.
        /// </summary>
        public string cvalue { get; set; }
        /// <summary>
        /// Credentials password.
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Session ID for security questions challenge. 
        /// </summary>
        public string securityQuestionSessionId { get; set; }
        /// <summary>
        /// Redemption token for security questions challenge. 
        /// </summary>
        public string securityQuestionRedemptionToken { get; set; }
        public string captchaId { get; set; }
        public string captchaToken { get; set; }
        public string captchaProvider { get; set; }
    }
}
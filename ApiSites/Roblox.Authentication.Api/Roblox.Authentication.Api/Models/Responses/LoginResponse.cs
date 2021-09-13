using Roblox.Web.Responses;

namespace Roblox.Authentication.Api.Models
{
    public class LoginResponse
    {
        /// <summary>
        /// The <see cref="SkinnyUserResponse"/>
        /// </summary>
        public SkinnyUserResponse user { get; set; }
        /// <summary>
        /// TwoStepVerification data if applicable
        /// </summary>
        public TwoStepVerificationSentResponse twoStepVerificationData { get; set; }
        /// <summary>
        /// IdentityVerificationLoginTicket if applicable
        /// </summary>
        public string identityVerificationLoginTicket { get; set; }
        /// <summary>
        /// Return if user is banned
        /// </summary>
        public bool isBanned { get; set; }
    }
}
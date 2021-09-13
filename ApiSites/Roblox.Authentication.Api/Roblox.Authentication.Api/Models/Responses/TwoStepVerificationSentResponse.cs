using Roblox.Platform.Authentication;

namespace Roblox.Authentication.Api.Models
{
    public class TwoStepVerificationSentResponse
    {
        /// <summary>
        /// The media type the two step verification code was sent on
        /// </summary>
        public CredentialsType mediaType { get; set; }
        /// <summary>
        /// The two step verification ticket 
        /// </summary>
        public string ticket { get; set; }
    }
}
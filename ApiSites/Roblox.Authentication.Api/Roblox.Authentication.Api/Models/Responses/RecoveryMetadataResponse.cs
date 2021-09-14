namespace Roblox.Authentication.Api.Models
{
    public class RecoveryMetadataResponse
    {
        /// <summary>
        /// Is client on phone
        /// </summary>
        public bool isOnPhone { get; set; }
        /// <summary>
        /// Length of phone code
        /// </summary>
        public int codeLength { get; set; }
        /// <summary>
        /// Is phone feature enabled for forgot username
        /// </summary>
        public bool isPhoneFeatureEnabledForUsername { get; set; }
        /// <summary>
        /// Is phone feature enabled for forgot password
        /// </summary>
        public bool isPhoneFeatureEnabledForPassword { get; set; }
        /// <summary>
        /// Is bedev2 captcha enabled for password reset
        /// </summary>
        public bool isBedev2CaptchaEnabledForPasswordReset { get; set; }
    }
}
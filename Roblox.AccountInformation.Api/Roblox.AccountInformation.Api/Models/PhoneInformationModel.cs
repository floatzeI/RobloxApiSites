namespace Roblox.AccountInformation.Api.Models
{
    public class PhoneResponse
    {
        /// <summary>The country Code</summary>
        public string countryCode { get; set; }
        public string prefix { get; set; }
        public string phone { get; set; }
        /// <summary>
        public bool isVerified { get; set; }
        /// <summary>Verification Code Length</summary>
        public int verificationCodeLength { get; set; } = 6;
    }
}
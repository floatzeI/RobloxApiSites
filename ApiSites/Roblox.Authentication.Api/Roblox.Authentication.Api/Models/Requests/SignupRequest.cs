using System.Collections.Generic;
using Roblox.Web.Enums;

namespace Roblox.Authentication.Api.Models
{
    public class SignupRequest
    {
        /// <summary>
        /// The username
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// The password
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public Gender gender { get; set; }
        /// <summary>
        /// Birthday
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// Whether the ToS agreement box was checked
        /// </summary>
        public bool isTosAgreementBoxChecked { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Locale
        /// </summary>
        public string locale { get; set; }
        /// <summary>
        /// The Asset IDs
        /// </summary>
        public IEnumerable<long> assetIds { get; set; }
        /// <summary>
        /// The Body Color Id
        /// </summary>
        public int bodyColorId { get; set; }
        /// <summary>
        /// The Body Type Scale
        /// </summary>
        public double bodyTypeScale { get; set; }
        /// <summary>
        /// The Head Scale
        /// </summary>
        public double headScale { get; set; }
        /// <summary>
        /// The Height Scale
        /// </summary>
        public double heightScale { get; set; }
        /// <summary>
        /// The Width Scale
        /// </summary>
        public double widthScale { get; set; }
        /// <summary>
        /// The Proportion Scale
        /// </summary>
        public double proportionScale { get; set; }
        /// <summary>
        /// Referral data
        /// </summary>
        public ReferralDataModel referralData { get; set; }
        /// <summary>
        /// AgreementIds
        /// </summary>
        public IEnumerable<string> agreementIds { get; set; }
        /// <summary>
        /// IdentityVerificationResultToken
        /// </summary>
        public string identityVerificationResultToken { get; set; }
        public string captchaId { get; set; }
        public string captchaToken { get; set; }
        public string captchaProvider { get; set; }
    }
}
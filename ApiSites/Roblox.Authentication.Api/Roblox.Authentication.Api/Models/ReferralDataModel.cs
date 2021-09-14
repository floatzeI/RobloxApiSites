using System;

namespace Roblox.Authentication.Api.Models
{
    public class ReferralDataModel
    {
        /// <summary>
        /// Acquisition Time
        /// </summary>
        public DateTime acquisitionTime { get; set; }
        /// <summary>
        /// Acquisition Referrer
        /// </summary>
        public string acquisitionReferrer { get; set; }
        /// <summary>
        /// Medium
        /// </summary>
        public string medium { get; set; } 
        /// <summary>
        /// Source
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// Campaign
        /// </summary>
        public string campaign { get; set; }
        /// <summary>
        /// AdGroup
        /// </summary>
        public string adGroup { get; set; }
        /// <summary>
        /// Keyword
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// SendInfo
        /// </summary>
        public int sendInfo { get; set; }
        /// <summary>
        /// Request Session Id
        /// </summary>
        public string requestSessionId { get; set; }
        /// <summary>
        /// Offer Id
        /// </summary>
        public string offerId { get;set; }
    }
}
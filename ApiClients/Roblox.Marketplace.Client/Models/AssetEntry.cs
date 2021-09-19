using System;

namespace Roblox.Marketplace.Client.Models
{
    public class AssetEntry
    {
        public long productId { get; set; }
        public long assetId { get; set; }
        public int? priceInRobux { get; set; }
        public int? priceInTickets { get; set; }
        public bool isForSale { get; set; }
        public bool isLimited { get; set; }
        public bool isLimitedUnique { get; set; }
        /// <summary>
        /// The limited unique stock. This is only allowed when isLimitedUnique is true
        /// </summary>
        public int? stockCount { get; set; }
        // roblox-only
        public DateTime? offSaleDeadline { get; set; }
        public int minimumMembershipLevel { get; set; }
        public int contentRatingId { get; set; }
        // ext
        public bool isFree => priceInRobux == null && priceInTickets == null;
    }
}
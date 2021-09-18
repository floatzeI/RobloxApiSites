using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roblox.Services.Models.Marketplace
{
    public class AssetEntry
    {
        public long productId { get; set; }
        public long assetId { get; set; }
        // general sale data
        public bool isForSale { get; set; }
        public int? priceInRobux { get; set; }
        public int? priceInTickets { get; set; }
        // limited data
        public bool isLimited { get; set; }
        public bool isLimitedUnique { get; set; }
        public int? stockCount { get; set; }
        // roblox-only
        public DateTime? offSaleDeadline { get; set; }
        public int minimumMembershipLevel { get; set; }
        public int contentRatingId { get; set; }

        public object ToDbObject()
        {
            return new
            {
                id = productId,
                asset_id = assetId,
                is_for_sale = isForSale,
                price_robux = priceInRobux,
                price_tickets = priceInTickets,
                stock_count = stockCount,
                off_sale_deadline = offSaleDeadline,
                minimum_membership_level = minimumMembershipLevel,
                content_rating_id = contentRatingId,
                is_limited = isLimited,
                is_limited_unique = isLimitedUnique,
            };
        }
    }
}
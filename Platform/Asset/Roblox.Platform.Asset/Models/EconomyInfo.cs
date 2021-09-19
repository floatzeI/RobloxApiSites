using System;

namespace Roblox.Platform.Asset.Models
{
    public class EconomyInfo
    {
        public int? priceInRobux { get; set; }
        public int? priceInTickets { get; set; }
        public bool isForSale { get; set; }
        public bool isLimited { get; set; }
        public bool isLimitedUnique { get; set; }
        public DateTime? offSaleDeadline { get; set; }
    }
}
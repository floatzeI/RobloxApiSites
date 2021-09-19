using System;

namespace Roblox.Services.Models.Ownership
{
    public class OwnershipEntry
    {
        public long userAssetId { get; set; }
        public long assetId { get; set; }
        public long userId { get; set; }
        public int? price { get; set; }
        public int? serialNumber { get; set; }
        public DateTime? expires { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
using System;

namespace Roblox.Services.Models.Assets
{
    public class AssetEntry
    {
        public long assetId { get; set; }
        public int assetTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int creatorType { get; set; }
        public long creatorId { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
using System;

namespace Roblox.Services.Models.Assets
{
    public class InsertAssetRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public int creatorType { get; set; }
        public long creatorId { get; set; }
        public int assetType { get; set; }
    }

    public class InsertAssetResponse
    {
        public long assetId { get; set; }
        public DateTime created { get; set; }
    }
}
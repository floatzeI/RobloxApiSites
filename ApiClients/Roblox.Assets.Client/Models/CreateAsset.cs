using Roblox.Web.Enums;

namespace Roblox.Assets.Client.Models
{
    public class CreateAssetRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public CreatorType creatorType { get; set; }
        public long creatorId { get; set; }
        public AssetType assetType { get; set; }
    }

    public class CreateAssetResponse
    {
        public long assetId { get; set; }
    }

}
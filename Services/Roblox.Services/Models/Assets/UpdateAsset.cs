namespace Roblox.Services.Models.Assets
{
    public class UpdateAssetRequest
    {
        public long assetId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int creatorType { get; set; }
        public long creatorId { get; set; }
    }
}
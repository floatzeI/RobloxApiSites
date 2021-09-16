namespace Roblox.Services.Models.AssetVersions
{
    public class InsertAssetVersionRequest
    {
        public long assetId { get; set; }
        public long userId { get; set; }
        public string fileId { get; set; }
    }
}
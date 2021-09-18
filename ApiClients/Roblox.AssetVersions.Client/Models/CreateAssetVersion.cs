using System;

namespace Roblox.AssetVersions.Client.Models
{
    public class CreateAssetVersionRequest
    {
        public string fileId { get; set; }
        public long userId { get; set; }
        public long assetId { get; set; }
    }

    public class CreateAssetVersionResponse
    {
        public long assetVersionId { get; set; }
        public int versionNumber { get; set; }
        public DateTime created { get; set; }
    }
}
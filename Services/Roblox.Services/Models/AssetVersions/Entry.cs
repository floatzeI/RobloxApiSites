using System;

namespace Roblox.Services.Models.AssetVersions
{
    public class AssetVersionEntry
    {
        public long assetVersionId { get; set; }
        public long assetId { get; set; }
        /// <summary>
        /// The userId who created the version
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// The version of the item, incrementing each time a new asset version is inserted for the assetId
        /// </summary>
        public int versionNumber { get; set; }
        /// <summary>
        /// ID of the file in Roblox.Files.Service
        /// </summary>
        /// <remarks>128 characters long, hex</remarks>
        public string fileId { get; set; }
        public DateTime created { get; set; }
    }
}
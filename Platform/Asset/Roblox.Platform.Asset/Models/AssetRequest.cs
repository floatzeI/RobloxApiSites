using System.Collections;
using System.Collections.Generic;
using System.IO;
using Roblox.Web.Enums;

namespace Roblox.Platform.Asset.Models
{
    public class CreateAssetRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public AssetType assetType { get; set; }
        public CreatorType creatorType { get; set; }
        public long creatorId { get; set; }
        /// <summary>
        /// Genres for the assetId. Optional.
        /// </summary>
        public IEnumerable<AssetGenre> genres { get; set; }
        /// <summary>
        /// Economy info. Optional.
        /// </summary>
        public EconomyInfo economyInfo { get; set; }
        /// <summary>
        /// The asset file (e.g. rbxml, png, jpg, etc)
        /// </summary>
        public Stream file { get; set; }
    }

    public class CreateAssetResponse
    {
        public long assetId { get; set; }
        public long assetVersionId { get; set; }
        public long productId { get; set; }
    }
}
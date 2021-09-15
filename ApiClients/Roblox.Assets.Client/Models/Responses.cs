using System;
using Roblox.Web.Enums;

namespace Roblox.Assets.Client.Models
{
    public class AssetDetailsEntry
    {
        public long assetId { get; set; }
        public string name { get; set; }
        public CreatorType creatorType { get; set; }
        public long creatorId { get; set; }
        public AssetType assetType { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
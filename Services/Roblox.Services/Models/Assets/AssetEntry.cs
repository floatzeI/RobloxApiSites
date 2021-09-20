using System;

namespace Roblox.Services.Models.Assets
{
    public class AssetEntry
    {
        public long assetId { get; set; }

        private int _assetTypeId { get; set; }
        public int assetTypeId
        {
            get => _assetTypeId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid AssetTypeId: 0");
                }

                _assetTypeId = value;
            }
        }

        public string name { get; set; }
        public string description { get; set; }
        public int creatorType { get; set; }
        public long creatorId { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
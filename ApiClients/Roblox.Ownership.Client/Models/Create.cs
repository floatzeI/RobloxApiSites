using System;

namespace Roblox.Ownership.Client
{
    public class CreateRequest
    {
        public long assetId { get; set; }
        public long userId { get; set; }
        public int? serialNumber { get; set; }
        public DateTime? expires { get; set; }
    }
}
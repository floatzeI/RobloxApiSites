using System;

namespace Roblox.Services.Models.Ownership
{
    public class CreateRequest
    {
        public long assetId { get; set; }
        public long userId { get; set; }
        public int? serialNumber { get; set; }
        public DateTime? expires { get; set; }
    }
}
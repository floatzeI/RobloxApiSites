using System.Collections;
using System.Collections.Generic;

namespace Roblox.Services.Models.Assets
{
    public class UpdateAssetGenresRequest
    {
        public long assetId { get; set; }
        public IEnumerable<int> genres { get; set; }
    }
}
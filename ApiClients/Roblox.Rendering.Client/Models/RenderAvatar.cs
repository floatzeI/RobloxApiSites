using System.Collections.Generic;

namespace Roblox.Rendering.Client.Models
{
    public class RenderAvatarResponse
    {
        public string fileBase64 { get; set; }
    }

    public class RenderAssetEntry
    {
        public long id { get; set; }
    }

    public class RenderBodyColors
    {
        public int headColorId { get; set; }
        public int torsoColorId { get; set; }
        public int rightArmColorId { get; set; }
        public int leftArmColorId { get; set; }
        public int rightLegColorId { get; set; }
        public int leftLegColorId { get; set; }
    }

    public class RenderAvatarRequest
    {
        public long userId { get; set; }
        public RenderBodyColors bodyColors { get; set; }
        public string playerAvatarType { get; set; }
        public IEnumerable<RenderAssetEntry> assets { get; set; }
    }
}
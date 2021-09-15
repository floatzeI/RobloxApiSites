using System.Collections.Generic;
using Roblox.Web.Enums;

namespace Roblox.Avatar.Client.Models
{
    public class AvatarColor
    {
        public int headColorId { get; set; }
        public int torsoColorId { get; set; }
        public int leftArmColorId { get; set; }
        public int rightArmColorId { get; set; }
        public int leftLegColorId { get; set; }
        public int rightLegColorId { get; set; }
    }

    public class AvatarScale
    {
        public decimal height { get; set; }
        public decimal width { get; set; }
        public decimal head { get; set; }
        public decimal depth { get; set; }
        public decimal proportion { get; set; }
        public decimal bodyType { get; set; }
    }

    public class AvatarEntry
    {
        public AvatarType type { get; set; }
        public AvatarColor colors { get; set; }
        public AvatarScale scales { get; set; }
        public IEnumerable<long> assetIds { get; set; }
    }
}
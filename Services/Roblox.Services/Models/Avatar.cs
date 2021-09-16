using System.Collections.Generic;

namespace Roblox.Services.Models.Avatar
{
    public enum AvatarType
    {
        R6 = 1,
        R15,
    }
    
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

    public class SetAvatarRequest : AvatarEntry
    {
        public long userId { get; set; }

        public object ToDatabaseEntry()
        {
            return new
            {
                user_id = userId,
                head_color_id = colors.headColorId,
                torso_color_id = colors.torsoColorId,
                left_arm_color_id = colors.leftArmColorId,
                right_arm_color_id = colors.rightArmColorId,
                left_leg_color_id = colors.leftLegColorId,
                right_leg_color_id = colors.rightLegColorId,
                height_scale = scales.height,
                width_scale = scales.width,
                head_scale = scales.head,
                depth_scale = scales.depth,
                proportion_scale = scales.proportion,
                body_type_scale = scales.bodyType,
                avatar_type = type,
            };
        }
    }

    public class DbAvatarEntry : AvatarColor
    {
        public AvatarType type { get; set; }
        public decimal height { get; set; }
        public decimal width { get; set; }
        public decimal head { get; set; }
        public decimal depth { get; set; }
        public decimal proportion { get; set; }
        public decimal bodyType { get; set; }
    }

    public class DbAssetEntry
    {
        public long assetId { get; set; }
    }
}
using System.Collections.Generic;
using Roblox.Web.Enums;

namespace Roblox.Platform.Avatar
{
    public class AvatarScaleRules
    {
        public ScaleRule height = new()
        {
            minimum = 0.9m,
            maximum = 1.05m,
            increment = 0.01m,
        };

        public ScaleRule width = new()
        {
            minimum = 0.7m,
            maximum = 1m,
            increment = 0.01m,
        };

        public ScaleRule head = new()
        {
            minimum = 0.9m,
            maximum = 1m,
            increment = 0.01m,
        };

        public ScaleRule proportion = new()
        {
            minimum = 0,
            maximum = 1,
            increment = 0.01m,
        };

        public ScaleRule bodyType = new()
        {
            minimum = 0,
            maximum = 1,
            increment = 0.01m,
        };
    }
    
    public static class AvatarValidator
    {
        // rules
        public static AvatarScaleRules scaleRules = new();

        private static readonly List<AssetType> _wearableAssetTypes = new()
        {
            AssetType.TeeShirt,
            AssetType.Hat,
            AssetType.Shirt,
            AssetType.Pants,
            AssetType.Head,
            AssetType.Face,
            AssetType.Gear,
            AssetType.Torso,
            AssetType.RightArm,
            AssetType.LeftArm,
            AssetType.RightLeg,
            AssetType.LeftLeg,
            AssetType.HairAccessory,
            AssetType.FaceAccessory,
            AssetType.NeckAccessory,
            AssetType.ShoulderAccessory,
            AssetType.FrontAccessory,
            AssetType.BackAccessory,
            AssetType.WaistAccessory,
            // todo: animations, new accessory types...
        };
            
        /// <summary>
        /// Get whether the specified <see cref="AssetType"/> can be worn
        /// </summary>
        /// <param name="assetType">The <see cref="AssetType"/> to check</param>
        /// <returns>True if the item can be worn, false otherwise</returns>
        public static bool IsWearable(AssetType assetType)
        {
            return _wearableAssetTypes.Contains(assetType);
        }

        public static bool IsScaleValid(ScaleRule rule, decimal userValue)
        {
            // If incrementCheck has decimals, then the increment is bad
            var incrementCheck = userValue / rule.increment;
            if (incrementCheck % 1 != 0) return false;
            
            return userValue >= rule.minimum && userValue <= rule.maximum;
        }
    }
}
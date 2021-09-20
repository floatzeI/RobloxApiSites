using System;

namespace Roblox.Marketplace.Client.Model
{
    public class ProductNotFoundForAsset : Exception
    {
        public ProductNotFoundForAsset(long assetId, Exception inner) : base("A product could not be found for this assetId.\nID = " + assetId, inner)
        {
            
        }
    }
}
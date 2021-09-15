using System;

namespace Roblox.Assets.Client.Exceptions
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(long assetId, Exception inner) : base("Asset does not exist\nID = " + assetId,
            inner)
        {
            
        }
    }
}
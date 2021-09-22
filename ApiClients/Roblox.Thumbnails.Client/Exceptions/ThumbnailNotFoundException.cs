using System;

namespace Roblox.Thumbnails.Client
{
    public class ThumbnailNotFoundException : Exception
    {
        public ThumbnailNotFoundException(long referenceId, Exception inner) : base(
            "Thumbnail not found with Reference ID = " + referenceId, inner)
        {
            
        }
        
        public ThumbnailNotFoundException(string thumbnailHash, Exception inner) : base(
            "Thumbnail not found with Hash = " + thumbnailHash, inner)
        {
            
        }
    }
}
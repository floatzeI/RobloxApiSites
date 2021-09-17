using System.IO;

namespace Roblox.Platform.Thumbnail.Models
{
    public class ThumbnailUploadRequest
    {
        /// <summary>
        /// A unique identifier. It must be 32 characters and in hex format.
        /// </summary>
        /// <remarks>
        /// Some examples could include a hash of the data required to make the thumbnail (e.g. an md5 hash of a json object representing a player thumbnail), or the md5 hash of the image itself
        /// </remarks>
        public string id { get; set; }
        public Stream file { get; set; }
        public string mime { get; set; }
        public ThumbnailType thumbnailType { get; set; }
    }
}
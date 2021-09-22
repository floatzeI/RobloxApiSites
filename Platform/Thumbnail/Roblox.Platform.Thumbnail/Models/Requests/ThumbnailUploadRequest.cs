using System.IO;
using Roblox.Thumbnails.Client.Models;

namespace Roblox.Platform.Thumbnail.Models
{
    public class ThumbnailUploadRequest
    {
        public ThumbnailUploadRequest(string id, Stream file, string mime, ThumbnailType thumbnailType, long referenceId, int resolutionX, int resolutionY)
        {
            this.id = id;
            this.file = file;
            this.mime = mime;
            this.thumbnailType = thumbnailType;
            this.referenceId = referenceId;
            this.resolutionX = resolutionX;
            this.resolutionY = resolutionY;
        }

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
        public long referenceId { get; set; }
        public int resolutionX { get; set; }
        public int resolutionY { get; set; }
    }
}
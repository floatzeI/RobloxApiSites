namespace Roblox.Services.Models.Thumbnails
{
    public class ThumbnailEntry
    {
        public string thumbnailId { get; set; }
        public long referenceId { get; set; }
        public string fileId { get; set; }
        public int thumbnailType { get; set; }
        public int resolutionX { get; set; }
        public int resolutionY { get; set; }
    }
}
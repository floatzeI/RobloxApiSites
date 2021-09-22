namespace Roblox.Thumbnails.Client.Models
{
    public class ThumbnailEntry
    {
        public ThumbnailEntry(string thumbnailId, long referenceId, string fileId, ThumbnailType thumbnailType,
            int resolutionX, int resolutionY)
        {
            this.thumbnailId = thumbnailId;
            this.referenceId = referenceId;
            this.fileId = fileId;
            this.thumbnailType = thumbnailType;
            this.resolutionX = resolutionX;
            this.resolutionY = resolutionY;
        }
        public string thumbnailId { get; set; }
        public long referenceId { get; set; }
        public string fileId { get; set; }
        public ThumbnailType thumbnailType { get; set; }
        public int resolutionX { get; set; }
        public int resolutionY { get; set; }
    }
}
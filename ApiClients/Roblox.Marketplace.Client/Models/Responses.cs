namespace Roblox.Marketplace.Client.Models
{
    public class ProductEntry
    {
        public long productId { get; set; }
        public long assetId { get; set; }
        public long? priceInRobux { get; set; }
        public long? priceInTickets { get; set; }
        public bool isFree { get; set; }
        public bool isForSale { get; set; }
        public bool isPublicDomain { get; set; }
    }
}
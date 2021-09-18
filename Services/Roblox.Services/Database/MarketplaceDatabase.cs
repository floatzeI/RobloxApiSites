using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Marketplace;

namespace Roblox.Services.Database
{
    public class MarketplaceDatabase : IMarketplaceDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        
        public MarketplaceDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }

        private const string _selectionColumns = "id as productId, asset_id as assetId, is_for_sale as isForSale, is_limited as isLimited, is_limited_unique as isLimitedUnique, price_robux as priceInRobux, price_tickets as priceInTickets, stock_count as stockCount, off_sale_deadline as offSaleDeadline, minimum_membership_level as minimumMembershipLevel, content_rating_id as contentRatingId";

        public async Task<AssetEntry> GetProduct(long productId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<Models.Marketplace.AssetEntry>(
                "SELECT "+_selectionColumns+" FROM marketplace WHERE id = @id", new
                {
                    id = productId,
                });
        }

        public async Task<AssetEntry> GetProductByAssetId(long assetId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<Models.Marketplace.AssetEntry>(
                "SELECT "+_selectionColumns+" FROM marketplace WHERE asset_id = @asset_id LIMIT 1", new
                {
                    asset_id = assetId,
                });
        }

        public async Task<long> InsertProduct(AssetEntry request)
        {
            var response = await db.connection.QuerySingleOrDefaultAsync(
                "INSERT INTO marketplace (asset_id, is_for_sale, price_robux, price_tickets, stock_count, off_sale_deadline, minimum_membership_level, content_rating_id) VALUES (@asset_id, @is_for_sale, @price_robux, @price_tickets, @stock_count, @off_sale_deadline, @minimum_membership_level, @content_rating_id) RETURNING id",
                request.ToDbObject());
            return (long)response.id;
        }

        public async Task UpdateProduct(AssetEntry request)
        {
            await db.connection.ExecuteAsync(
                "UPDATE marketplace SET is_for_sale = @is_for_sale, price_robux = @price_robux, price_tickets = @price_tickets, stock_count = @stock_count, off_sale_deadline = @off_sale_deadline, minimum_membership_level = @minimum_membership_level, content_rating_id = @content_rating_id WHERE id = @id",
                request.ToDbObject());
        }
    }
}
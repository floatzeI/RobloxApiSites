using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Thumbnails;

namespace Roblox.Services.Database
{
    public class ThumbnailsDatabase : IThumbnailsDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }

        public ThumbnailsDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }

        private const string _columnSelection = "id as thumbnailId, reference_id as referenceId, file_id as fileId, thumbnail_type as thumbnailType, resolution_x as resolution, resolution_y as resolutionY";

        public async Task InsertThumbnail(ThumbnailEntry request)
        {
            await db.connection.ExecuteAsync(
                "INSERT INTO thumbnail (id, file_id, reference_id, thumbnail_type, resolution_x, resolution_y) VALUES (@id, @file_id, @reference_id, @thumbnail_type, @resolution_x, @resolution_y)",
                new
                {
                    id = request.thumbnailId,
                    file_id = request.fileId,
                    reference_id = request.referenceId,
                    thumbnail_type = request.thumbnailType,
                    resolution_x = request.resolutionX,
                    resolution_y = request.resolutionY,
                });
        }

        public async Task<ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType)
        {
            return await db.connection.QuerySingleOrDefaultAsync<ThumbnailEntry>(
                "SELECT "+_columnSelection+" FROM thumbnail WHERE reference_id = @id AND thumbnail_type = @thumbnail_type LIMIT 1",
                new
                {
                    id = referenceId,
                    thumbnail_type = thumbnailType,
                });
        }

        public async Task<ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType, int resolutionX, int resolutionY)
        {
            return await db.connection.QuerySingleOrDefaultAsync<ThumbnailEntry>(
                "SELECT "+_columnSelection+" FROM thumbnail WHERE reference_id = @id AND thumbnail_type = @thumbnail_type AND resolution_x = @resolution_x AND resolution_y = @resolution_y LIMIT 1",
                new
                {
                    id = referenceId,
                    thumbnail_type = thumbnailType,
                    resolution_x = resolutionX,
                    resolution_y = resolutionY,
                });
        }

        public async Task<ThumbnailEntry> ResolveThumbnailHash(string thumbnailHash, int resolutionX, int resolutionY)
        {
            return await db.connection.QuerySingleOrDefaultAsync<ThumbnailEntry>(
                "SELECT "+_columnSelection+" FROM thumbnail WHERE id = @id AND resolution_x = @resolution_x AND resolution_y = @resolution_y LIMIT 1",
                new
                {
                    id = thumbnailHash,
                    resolution_x = resolutionX,
                    resolution_y = resolutionY,
                });
        }
    }
}
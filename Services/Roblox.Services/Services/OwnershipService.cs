using System.Linq;
using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Models.Ownership;

namespace Roblox.Services.Services
{
    public class OwnershipService : IOwnershipService
    {
        private IOwnershipDatabase db { get; set; }

        public OwnershipService(IOwnershipDatabase db)
        {
            this.db = db;
        }
        
        public async Task<OwnershipEntry> InsertEntry(CreateRequest request)
        {
            return await db.InsertEntry(request);
        }

        public async Task<bool> DoesUserOwnsAsset(long userId, long assetId)
        {
            var entries = await db.GetEntriesByUser(userId, assetId);
            return entries.Any();
        }
    }
}
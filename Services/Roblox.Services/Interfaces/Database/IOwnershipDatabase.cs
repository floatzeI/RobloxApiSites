using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IOwnershipDatabase
    {
        Task<Models.Ownership.OwnershipEntry> InsertEntry(Models.Ownership.CreateRequest request);
        Task<IEnumerable<Models.Ownership.OwnershipEntry>> GetEntriesByUser(long userId, long assetId);
    }
}
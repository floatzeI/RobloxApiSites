using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IOwnershipService
    {
        Task<Models.Ownership.OwnershipEntry> InsertEntry(Models.Ownership.CreateRequest request);
        Task<bool> DoesUserOwnsAsset(long userId, long assetId);
    }
}
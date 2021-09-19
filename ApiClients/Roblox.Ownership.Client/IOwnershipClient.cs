using System.Threading.Tasks;

namespace Roblox.Ownership.Client
{
    public interface IOwnershipClient
    {
        Task<OwnershipEntry> CreateEntry(CreateRequest request);
    }
}
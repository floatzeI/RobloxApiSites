using System.Threading.Tasks;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Database
{
    public interface IUsersDatabase
    {
        Task<AccountInformationEntry> GetAccountInformationEntry(long userId);
        Task<bool> DoesHaveAccountInformationEntry(long userId);
        Task InsertAccountInformationEntry(Models.Users.AccountInformationEntry entry);
        Task UpdateUserDescription(long userId, string description);
    }
}
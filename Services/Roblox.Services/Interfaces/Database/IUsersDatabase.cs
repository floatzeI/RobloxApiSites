using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Database
{
    public interface IUsersDatabase
    {
        Task<IAsyncDisposable> CreateLock(string resource, TimeSpan maxWaitTime);
        Task<AccountInformationEntry> GetAccountInformationEntry(long userId);
        Task<bool> DoesHaveAccountInformationEntry(long userId);
        Task InsertAccountInformationEntry(Models.Users.AccountInformationEntry entry);
        Task UpdateUserBirthDate(long userId, DateTime birthDate);
        Task UpdateUserDescription(long userId, string description);
        Task<Models.Users.UserAccountEntry> GetUserAccountById(long userId);
        Task<Models.Users.UserAccountEntry> InsertUser(string username);
        /// <summary>
        /// Get users by username, case-insensitive
        /// </summary>
        /// <param name="username">The username to lookup</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="SkinnyUserAccountEntry"/></returns>
        Task<IEnumerable<Models.Users.SkinnyUserAccountEntry>> GetUsersByUsername(string username);

        Task DeleteUser(long userId);
    }
}
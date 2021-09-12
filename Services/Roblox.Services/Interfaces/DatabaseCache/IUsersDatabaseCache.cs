using System;
using System.Threading.Tasks;
using Roblox.Services.Models.Users;

namespace Roblox.Services.DatabaseCache
{
    public interface IUsersDatabaseCache
    {
        /// <summary>
        /// Get cached account information, or null if not in cache.
        /// </summary>
        /// <param name="userId">The accountId to get information for</param>
        /// <returns>The <see cref="AccountInformationEntry"/>, or null</returns>
        Task<AccountInformationEntry> GetAccountInformation(long userId);
        
        /// <summary>
        /// Set the user's account information cache to the specified entry.
        /// </summary>
        /// <param name="entry">The entry to set the cache key to.</param>
        Task SetAccountInformation(AccountInformationEntry entry);

        /// <summary>
        /// Delete cache entry for the userId
        /// </summary>
        /// <param name="userId">The userId to delete the accountinformation for</param>
        Task DeleteAccountInformation(long userId);

        /// <summary>
        /// Set the accountInformation cache description entry for the userId. This will do nothing if the AccountInformation entry is not already cached.
        /// </summary>
        /// <param name="userId">The userId to set the description for</param>
        /// <param name="description">The account description</param>
        /// <param name="updatedAt">The date the description was updated</param>
        Task SetDescription(long userId, string description, DateTime updatedAt);
    }
}
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IPasswordsDatabase
    {
        /// <summary>
        /// Get a password entry corresponding to the userId. Returns null if no entry exists.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Models.Passwords.UserAccountPasswordEntry> GetPasswordEntryForUser(long accountId);
        /// <summary>
        /// Insert a password entry for the user
        /// </summary>
        /// <param name="accountId">The User's ID</param>
        /// <param name="passwordHash">The password hash</param>
        Task InsertPassword(long accountId, string passwordHash);
        /// <summary>
        /// Set the password hash for the user
        /// </summary>
        /// <param name="accountId">The user's ID</param>
        /// <param name="passwordHash">The password hash</param>
        Task SetPassword(long accountId, string passwordHash);
    }
}
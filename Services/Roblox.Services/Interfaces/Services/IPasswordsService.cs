using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IPasswordsService
    {
        /// <summary>
        /// Confirm the password provided matches the stored password for the user.
        /// </summary>
        /// <param name="accountId">The userId</param>
        /// <param name="password">The password, not hashed</param>
        /// <returns>True if the password is correct, false otherwise</returns>
        Task<bool> IsPasswordCorrectForUser(long accountId, string password);
        
        /// <summary>
        /// Set the password for the user.
        /// </summary>
        /// <param name="accountId">The userId to insert a password for</param>
        /// <param name="password">The password, not hashed</param>
        Task SetPasswordForUser(long accountId, string password);
    }
}
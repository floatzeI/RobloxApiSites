using System.Threading.Tasks;
using Roblox.Passwords.Client.Exceptions;

namespace Roblox.Passwords.Client
{
    public interface IPasswordsV1Client
    {
        /// <summary>
        /// Get whether the password supplied is correct
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <param name="userProvidedPassword">The password provided by the user</param>
        /// <returns>true if the password is correct, false otherwise</returns>
        /// <exception cref="UserHasNoPasswordException">User does not have a password set</exception>
        Task<bool> IsPasswordCorrect(long accountId, string userProvidedPassword);
    }
}
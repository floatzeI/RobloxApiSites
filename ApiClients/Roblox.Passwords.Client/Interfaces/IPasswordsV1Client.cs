using System.Threading.Tasks;

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
        Task<bool> IsPasswordCorrect(long accountId, string userProvidedPassword);
    }
}
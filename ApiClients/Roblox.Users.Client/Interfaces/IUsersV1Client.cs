using System.Threading.Tasks;
using Roblox.Users.Client.Models.Responses;
using Roblox.Web.Enums;

namespace Roblox.Users.Client
{
    public interface IUsersV1Client
    {
        /// <summary>
        /// Get a user's Description entry by their userId
        /// </summary>
        /// <param name="agentId">The ID of the user to fetch the description record for</param>
        /// <returns>The user's <see cref="Roblox.Users.Client.Models.Responses.DescriptionResponse" /></returns>
        Task<DescriptionResponse> GetDescription(long agentId);
        
        /// <summary>
        /// Set the description for the user
        /// </summary>
        /// <param name="agentId">The user to set the description for</param>
        /// <param name="newDescription">The user's new description</param>
        Task SetDescription(long agentId, string newDescription);

        /// <summary>
        /// Get a user by their userId
        /// </summary>
        /// <param name="agentId">The ID of the user</param>
        /// <returns>The <see cref="GetUserByIdEntry" /></returns>
        Task<Models.Responses.GetUserByIdEntry> GetUserById(long agentId);

        /// <summary>
        /// Get a user by their username. This is case-insensitive, and does not check previous usernames.
        /// </summary>
        /// <param name="username">The username to lookup</param>
        /// <returns>The <see cref="SkinnyUserEntry"/></returns>
        /// <exception cref="Exceptions.UserNotFoundException">User could not be found</exception>
        Task<Models.Responses.SkinnyUserEntry> GetUserByUsername(string username);

        /// <summary>
        /// Add a user to the store.
        /// </summary>
        /// <param name="username">The username to create the user with</param>
        /// <param name="birthYear">The year the user was born</param>
        /// <param name="birthMonth">The month the user was born</param>
        /// <param name="birthDay">The day the user was born</param>
        /// <returns>The <see cref="GetUserByIdEntry"/> for the newly created user</returns>
        Task<GetUserByIdEntry> InsertUser(string username, int birthYear, int birthMonth, int birthDay);
        
        /// <summary>
        /// Delete a user by their ID. The user must have been created less than 10 minutes ago.
        /// </summary>
        /// <param name="userId">The ID of the account to delete</param>
        Task DeleteUser(long userId);
        
        Task SetGender(long userId, Gender gender);
    }
}
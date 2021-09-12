using System.Threading.Tasks;
using Roblox.Users.Client.Models.Responses;

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
    }
}
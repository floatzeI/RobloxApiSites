namespace Roblox.Authentication.Api.Models
{
    public class SignupResponse
    {
        /// <summary>
        /// The user ID
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// The id of the game to start with
        /// </summary>
        public long starterPlaceId { get; set; }
    }
}
using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IUsersService
    {
        Task<Models.Users.UserDescriptionEntry> GetDescription(long userId);
    }
}
using System.Threading.Tasks;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Services
{
    public interface ISessionsService
    {
        Task<CreateSessionResponse> CreateSession(long userId);
    }
}
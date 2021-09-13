using System.Threading.Tasks;

namespace Roblox.Sessions.Client
{
    public interface ISessionsV1Client
    {
        Task<Models.Responses.GetSessionByIdResponse> GetSessionById(string sessionId);
    }
}
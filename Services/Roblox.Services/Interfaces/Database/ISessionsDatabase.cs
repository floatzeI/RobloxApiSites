using System;
using System.Threading.Tasks;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Database
{
    public interface ISessionsDatabase
    {
        Task<SessionEntry> InsertSession(long userId, string sessionId);
        Task DeleteSession(string sessionId);
        Task<SessionEntry> GetSession(string sessionId);
        Task SetSessionUpdatedAt(string sessionId, DateTime updatedAt);
    }
}
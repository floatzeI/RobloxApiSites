using System;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Database
{
    public class SessionsDatabase : ISessionsDatabase
    {
        public async Task<SessionEntry> InsertSession(long userId, string sessionId)
        {
            var currentTime = DateTime.Now;
            await Db.client.ExecuteAsync("INSERT INTO account_session (user_id, id, created_at, updated_at) VALUES (@user_id, @id, @created_at, @updated_at)", new
            {
                user_id = userId,
                id = sessionId,
                created_at = currentTime,
                updated_at = currentTime,
            });
            return new()
            {
                userId = userId,
                id = sessionId,
                updated = currentTime,
                created = currentTime,
            };
        }

        public async Task DeleteSession(string sessionId)
        {
            await Db.client.ExecuteAsync("DELETE FROM account_session WHERE id = @id", new
            {
                id = sessionId,
            });
        }
    }
}
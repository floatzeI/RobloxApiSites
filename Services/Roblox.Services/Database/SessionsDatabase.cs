using System;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Database
{
    public class SessionsDatabase : ISessionsDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        public SessionsDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }
        
        public async Task<SessionEntry> InsertSession(long userId, string sessionId)
        {
            var currentTime = DateTime.Now;
            await db.connection.ExecuteAsync("INSERT INTO account_session (user_id, id, created_at, updated_at) VALUES (@user_id, @id, @created_at, @updated_at)", new
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
            await db.connection.ExecuteAsync("DELETE FROM account_session WHERE id = @id", new
            {
                id = sessionId,
            });
        }

        public async Task<SessionEntry> GetSession(string sessionId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<SessionEntry>(
                "SELECT id, user_id as userId, created_at as created, updated_at as updated FROM account_session WHERE id = @id", new
                {
                    id = sessionId,
                });
        }

        public async Task SetSessionUpdatedAt(string sessionId, DateTime updatedAt)
        {
            await db.connection.ExecuteAsync("UPDATE account_session SET updated_at = @updated_at WHERE id = @id", new
            {
                id = sessionId,
                updated_at = updatedAt,
            });
        }
    }
}
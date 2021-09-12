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
    }
}
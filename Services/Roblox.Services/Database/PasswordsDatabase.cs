using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Passwords;

namespace Roblox.Services.Database
{
    public class PasswordsDatabase : IPasswordsDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        public PasswordsDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }

        public async Task<UserAccountPasswordEntry> GetPasswordEntryForUser(long accountId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<UserAccountPasswordEntry>(
                "SELECT user_id as userId, password as passwordHash, password_status as passwordStatus, created_at as created, updated_at as updated FROM account_password WHERE user_id = @user_id LIMIT 1",
                new
                {
                    user_id = accountId,
                });
        }

        public async Task InsertPassword(long accountId, string passwordHash)
        {
            await db.connection.ExecuteAsync(
                "INSERT INTO account_password (user_id, password) VALUES (@user_id, @password)", new
                {
                    user_id = accountId,
                    password = passwordHash,
                });
        }

        public async Task SetPassword(long accountId, string passwordHash)
        {
            await db.connection.ExecuteAsync(
                "UPDATE account_password SET password = @password WHERE user_id = @user_id", new
                {
                    user_id = accountId,
                    password = passwordHash,
                });
        }
    }
}
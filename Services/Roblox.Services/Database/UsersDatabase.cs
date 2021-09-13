using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Database
{
    public class UsersDatabase : IUsersDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        private IUsersDatabaseCache dbCache { get; }
        public UsersDatabase(DatabaseConfiguration<IUsersDatabaseCache> config)
        {
            db = config.dbConnection;
            dbCache = config.dbCache;
        }
        
        public async Task<AccountInformationEntry> GetAccountInformationEntry(long userId)
        {
            var cache = await dbCache.GetAccountInformation(userId);
            if (cache != null) return cache;
            var result =
                await db.connection
                    .QuerySingleOrDefaultAsync<AccountInformationEntry>(@"SELECT 
                    user_id as userId, 
                    description, 
                    gender,
                    created_at as created, 
                    updated_at as updated,
                    EXTRACT(YEAR FROM birthdate) as birthYear,
                    EXTRACT(MONTH FROM birthdate) as birthMonth,
                    EXTRACT(DAY FROM birthdate) as birthDay
                FROM account_information 
                WHERE user_id = @user_id", new
                    {
                        user_id = userId,
                    });
            if (result == null) return null;
            
            await dbCache.SetAccountInformation(result);
            return result;
        }

        public async Task<bool> DoesHaveAccountInformationEntry(long userId)
        {
            var inCache = await dbCache.GetAccountInformation(userId);
            if (inCache != null) return true;
            var exists = await db.connection.QuerySingleOrDefaultAsync(
                "SELECT user_id FROM account_information WHERE user_id = @user_id", new
                {
                    user_id = userId,
                });
            return exists != null;
        }
        
        public async Task InsertAccountInformationEntry(Models.Users.AccountInformationEntry entry)
        {
            DateTime? birth = null;
            if (entry.birthYear != null && entry.birthDay != null && entry.birthMonth != null)
            {
                birth = new DateTime();
                birth = birth.Value.AddYears(entry.birthYear.Value);
                birth = birth.Value.AddMonths(entry.birthMonth.Value);
                birth = birth.Value.AddDays(entry.birthDay.Value);
            }
            await db.connection.ExecuteAsync("INSERT INTO account_information (user_id, birthdate, gender, description) VALUES (@user_id, @birthdate, @gender, @description)", new 
            {
                user_id = entry.userId,
                description = entry.description,
                birthdate = birth,
                gender = entry.gender,
            });
            await dbCache.SetAccountInformation(entry);
        }

        public async Task UpdateUserDescription(long userId, string description)
        {
            var updatedAt = DateTime.Now;
            await dbCache.SetDescription(userId, description, updatedAt);
            await db.connection.ExecuteAsync(
                "UPDATE account_information SET description = @description, updated_at = @updated_at WHERE user_id = @user_id", new
                {
                    user_id = userId,
                    description = description,
                    updated_at = updatedAt,
                });
        }

        public async Task<Models.Users.UserAccountEntry> GetUserAccountById(long userId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<Models.Users.UserAccountEntry>(
                "SELECT id as userId, username, display_name as displayName, account_status as accountStatus, created_at as created, updated_at as updated FROM account WHERE id = @id",
                new
                {
                    id = userId,
                });
        }
    }
}
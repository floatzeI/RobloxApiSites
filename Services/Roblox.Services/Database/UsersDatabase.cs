using System;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Database
{
    public class UsersDatabase : IUsersDatabase
    {
        public async Task<AccountInformationEntry> GetAccountInformationEntry(long userId)
        {
            var result =
                await Db.client
                    .QuerySingleOrDefaultAsync<AccountInformationEntry>(@"SELECT 
                    user_id as userId, 
                    description, 
                    created_at as created, 
                    updated_at as updated 
                FROM account_information 
                WHERE user_id = @user_id", new
                    {
                        user_id = userId,
                    });
            return result;
        }

        public async Task<bool> DoesHaveAccountInformationEntry(long userId)
        {
            var exists = await Db.client.QuerySingleOrDefaultAsync(
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
            await Db.client.ExecuteAsync("INSERT INTO account_information (user_id, birthdate, gender, description) VALUES (@user_id, @birthdate, @gender, @description)", new 
            {
                user_id = entry.userId,
                description = entry.description,
                birthdate = birth,
                gender = entry.gender,
            });
        }

        public async Task UpdateUserDescription(long userId, string description)
        {
            await Db.client.ExecuteAsync(
                "UPDATE account_information SET description = @description WHERE user_id = @user_id", new
                {
                    user_id = userId,
                });
        }
    }
}
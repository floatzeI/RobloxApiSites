using System;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Services
{
    public class UsersService : IUsersService
    {
        public async Task<UserDescriptionEntry> GetDescription(long userId)
        {
            var result =
                await Db.client
                    .QuerySingleOrDefaultAsync<Models.Users.UserDescriptionEntry>(@"SELECT 
                    user_id as userId, 
                    description, 
                    created_at as created, 
                    updated_at as updated 
                FROM account_information 
                WHERE user_id = @user_id", new
                    {
                        user_id = userId,
                    });
            if (result == null) throw new RecordNotFoundException(userId);

            return result;
        }
    }
}
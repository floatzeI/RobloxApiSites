using System;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Users;

namespace Roblox.Services.Services
{
    public class UsersService : IUsersService
    {
        private IUsersDatabase db { get; set; }

        public UsersService(IUsersDatabase db)
        {
            this.db = db;
        }
        
        public async Task<UserDescriptionEntry> GetDescription(long userId)
        {
            var result = await db.GetAccountInformationEntry(userId);
            if (result == null) throw new RecordNotFoundException(userId);

            return new()
            {
                userId = result.userId,
                description = result.description,
                created = result.created,
                updated = result.updated,
            };
        }
        
        public async Task SetUserDescription(long userId, string description)
        {
            var exists = await db.DoesHaveAccountInformationEntry(userId);
            if (exists)
            {
                await db.UpdateUserDescription(userId, description);
            }
            else
            {
                await db.InsertAccountInformationEntry(new()
                {
                    userId = userId,
                    description = description,
                    birthDay = null,
                    birthMonth = null,
                    birthYear = null,
                    gender = null,
                });
            }
        }

        public async Task<Models.Users.UserInformationResponse> GetUserById(long userId)
        {
            var userInfo = await db.GetUserAccountById(userId);
            if (userInfo == null) throw new RecordNotFoundException(userId);
            var accountInformation = await db.GetAccountInformationEntry(userId);
            return new()
            {
                userId = userInfo.userId,
                username = userInfo.username,
                displayName = userInfo.displayName,
                description = accountInformation.description,
                accountStatus = userInfo.accountStatus,
                created = userInfo.created,
            };
        }
    }
}
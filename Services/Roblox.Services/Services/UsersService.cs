using System;
using System.Linq;
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
        
        public async Task SetUserBirthDate(long userId, DateTime birthDate)
        {
            var exists = await db.DoesHaveAccountInformationEntry(userId);
            if (exists)
            {
                await db.UpdateUserBirthDate(userId, birthDate);
            }
            else
            {
                await db.InsertAccountInformationEntry(new()
                {
                    userId = userId,
                    description = null,
                    birthDay = birthDate.Day,
                    birthMonth = birthDate.Month,
                    birthYear = birthDate.Year,
                    gender = null,
                });
            }
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
                description = accountInformation?.description,
                accountStatus = userInfo.accountStatus,
                created = userInfo.created,
            };
        }

        public DateTime GetDateTimeFromBirthDate(int birthYear, int birthMonth, int birthDay)
        {
            try
            {
                var dt = new DateTime(birthYear, birthMonth, birthDay);
                return dt;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ParameterException("birthDay");
            }
        }

        public async Task<Models.Users.UserAccountEntry> CreateUser(string username)
        {
            var resource = "CreateUserWithName:v1:" + username.ToLower();
            await using var creationLock = await db.CreateLock(resource, TimeSpan.FromSeconds(5));
            
            var alreadyExists = await db.GetUsersByUsername(username);
            if (alreadyExists.Any()) throw new RecordAlreadyExistsException("username");
            
            var creation = await db.InsertUser(username);
            return creation;
        }

        public async Task DeleteUser(long userId)
        {
            await db.DeleteUser(userId);
        }
    }
}
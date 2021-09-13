using System;
using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IUsersService
    {
        Task<Models.Users.UserDescriptionEntry> GetDescription(long userId);
        Task SetUserDescription(long userId, string description);
        Task SetUserBirthDate(long userId, DateTime birthDate);
        Task<Models.Users.UserInformationResponse> GetUserById(long userId);
        DateTime GetDateTimeFromBirthDate(int birthYear, int birthMonth, int birthDay);
        Task<Models.Users.UserAccountEntry> CreateUser(string username);
        Task DeleteUser(long userId);
    }
}
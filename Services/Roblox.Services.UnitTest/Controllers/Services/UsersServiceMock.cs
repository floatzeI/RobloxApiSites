using System.Threading.Tasks;
using Roblox.Services.Models.Users;
using Roblox.Services.Services;

namespace Roblox.Services.UnitTest.Controllers.Services
{
    public class UsersServiceMock : IUsersService
    {
        public UserDescriptionEntry getDescriptionMockData { get; set; }
        public async Task<UserDescriptionEntry> GetDescription(long userId)
        {
            return getDescriptionMockData;
        }
    }
}
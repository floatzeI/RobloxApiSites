using System.Threading.Tasks;
using Roblox.Services.Models.AccountInformation;
using Roblox.Services.Services;

namespace Roblox.Services.UnitTest.Controllers.Services
{
    public class AccountInformationServiceMock : IAccountInformationService
    {
        public AccountDescriptionEntry getDescriptionMockData { get; set; }
        public async Task<AccountDescriptionEntry> GetDescription(long userId)
        {
            return getDescriptionMockData;
        }
    }
}
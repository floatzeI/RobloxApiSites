using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IAccountInformationService
    {
        Task<Models.AccountInformation.AccountDescriptionEntry> GetDescription(long userId);
    }
}
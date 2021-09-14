using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;

namespace Roblox.Services.Services
{
    public class PasswordsService : IPasswordsService
    {
        private IPasswordsDatabase db { get; set; }

        public PasswordsService(IPasswordsDatabase db)
        {
            this.db = db;
        }
        
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public async Task<bool> IsPasswordCorrectForUser(long accountId, string password)
        {
            var entry = await db.GetPasswordEntryForUser(accountId);
            if (entry == null) throw new RecordNotFoundException();
            return VerifyPassword(password, entry.passwordHash);
        }

        public async Task SetPasswordForUser(long accountId, string password)
        {
            var hash = HashPassword(password);
            var exists = await db.GetPasswordEntryForUser(accountId);
            if (exists == null)
            {
                await db.InsertPassword(accountId, hash);
            }
            else
            {
                await db.SetPassword(accountId, hash);
            }
        }
    }
}
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Roblox.Services.Models.Users;

namespace Roblox.Services.DatabaseCache
{
    public class UsersDatabaseCache : DatabaseCacheBase, IUsersDatabaseCache
    {
        private string accountInformationCacheKey = "AccountInformation:v1:";
        public async Task<AccountInformationEntry> GetAccountInformation(long userId)
        {
            var key = accountInformationCacheKey + userId;
            return await GetLock(key, async () =>
            {
                var result = await Redis.client.GetDatabase(0).StringGetAsync(key);
                return result.HasValue ? JsonSerializer.Deserialize<AccountInformationEntry>(result) : null;
            });
        }

        public async Task SetAccountInformation(AccountInformationEntry entry)
        {
            var key = accountInformationCacheKey + entry.userId;
            await GetLock(key, async () =>
            {
                await Redis.client.GetDatabase(0).StringSetAsync(key, JsonSerializer.Serialize(entry), TimeSpan.FromHours(1));
                return true;
            });
        }

        public async Task SetDescription(long userId, string description, DateTime updatedAt)
        {
            var key = accountInformationCacheKey + userId;
            await GetLock(key, async () =>
            {
                var db = Redis.client.GetDatabase(0);
                var existingEntry = await db.StringGetAsync(key);
                if (existingEntry.HasValue)
                {
                    var parse = JsonSerializer.Deserialize<AccountInformationEntry>(existingEntry);
                    if (parse == null) return false;
                    parse.description = description;
                    parse.updated = updatedAt;
                    await db.StringSetAsync(key, JsonSerializer.Serialize(parse));
                }
                return true;
            });
        }
    }
}
using System;
using System.Threading.Tasks;
using Roblox.Services.Exceptions.DatabaseCache;

namespace Roblox.Services.DatabaseCache
{
    public class DatabaseCacheBase
    {
        protected async Task<T> GetLock<T>(string resourceName, Func<Task<T>> cb)
        {
            await using var resource = await Redis.redlockFactory.CreateLockAsync(resourceName, TimeSpan.FromSeconds(60));
            if (resource.IsAcquired)
            {
                return await cb();
            }

            throw new LockTimeoutException("Could not acquire redlock for resource = " + resourceName);
        }
    }
}
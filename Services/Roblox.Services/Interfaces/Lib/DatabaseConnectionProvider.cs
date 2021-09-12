using System.Data;

namespace Roblox.Services
{
    public interface IDatabaseConnectionProvider
    {
        /// <summary>
        /// Get a db connection. This must return a new connection each time get() is called.
        /// </summary>
        IDbConnection connection { get; }
    }
}
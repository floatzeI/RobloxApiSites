using System.Data;

namespace Roblox.Services
{
    public class PostgresDatabaseProvider : IDatabaseConnectionProvider
    {
        public IDbConnection connection => Roblox.Services.Db.client;
    }
}
using Npgsql;

namespace Roblox.Services.Database
{
    public class DatabaseConfiguration<TDbCache>
    {
        public IDatabaseConnectionProvider dbConnection { get; }
        public TDbCache dbCache { get; }
        
        public DatabaseConfiguration(IDatabaseConnectionProvider dbConnection, TDbCache dbCache)
        {
            this.dbConnection = dbConnection;
            this.dbCache = dbCache;
        }
    }
}
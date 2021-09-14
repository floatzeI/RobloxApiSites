using Roblox.Services.DatabaseCache;

namespace Roblox.Services.Database
{
    public class AvatarDatabase : IAvatarDatabase
    {
        private IAvatarDatabaseCache cache { get; set; }
        private IDatabaseConnectionProvider db { get; set; }
        
        public AvatarDatabase(DatabaseConfiguration<IAvatarDatabaseCache> config)
        {
            db = config.dbConnection;
            cache = config.dbCache;
        }
    }
}
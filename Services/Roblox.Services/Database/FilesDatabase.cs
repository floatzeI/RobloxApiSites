namespace Roblox.Services.Database
{
    public class FilesDatabase : IFilesDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        
        public FilesDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }
    }
}
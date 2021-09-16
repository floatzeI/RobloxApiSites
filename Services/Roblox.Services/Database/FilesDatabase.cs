using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace Roblox.Services.Database
{
    public class FilesDatabase : IFilesDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        
        public FilesDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }

        public async Task InsertFile(string fileHash, string mimeType, long sizeInBytes)
        {
            await db.connection.ExecuteAsync("INSERT INTO file (id, mime, size_bytes) VALUES (@id, @mime, @size_bytes)",
                new
                {
                    id = fileHash,
                    mime = mimeType,
                    size_bytes = sizeInBytes,
                });
        }

        public async Task<bool> DoesFileExist(string fileHash)
        {
            var exists = await db.connection.QuerySingleOrDefaultAsync("SELECT id FROM file WHERE id = @id", new
            {
                id = fileHash,
            });
            return exists != null;
        }
    }
}
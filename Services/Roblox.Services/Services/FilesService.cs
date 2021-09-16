using Roblox.Services.Database;

namespace Roblox.Services.Services
{
    public class FilesService : IFilesService
    {
        private IFilesDatabase filesDatabase { get; set; }
        private IFilesStorageDatabase storageDatabase { get; set; }
        
        public FilesService(IFilesDatabase db, IFilesStorageDatabase storage)
        {
            filesDatabase = db;
            storageDatabase = storage;
        }
    }
}
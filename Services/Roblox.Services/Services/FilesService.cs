using Roblox.Services.Database;

namespace Roblox.Services.Services
{
    public class FilesService : IFilesService
    {
        private IFilesDatabase filesDatabase { get; set; }
        
        public FilesService(IFilesDatabase db)
        {
            filesDatabase = db;
        }
    }
}
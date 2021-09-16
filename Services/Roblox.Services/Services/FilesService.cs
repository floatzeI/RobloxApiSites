using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<string> CreateFileHash(Stream fileStream)
        {
            // Roblox seems to use MD5 with hex encoding. Example hashes:
            // - a0b598f5ff327c27bbb45034f37b61d7
            // - b3db0d0d676c858c0249acd93504df10
            // - 4df253f22f492b2199ba8c48d8cb0269
            using var md5 = MD5.Create();
            var hash = await md5.ComputeHashAsync(fileStream);
            return Convert.ToHexString(hash);
        }

        public async Task UploadFile(Stream fileStream, string fileHash, string mimeType)
        {
            await storageDatabase.UploadFile(fileHash, fileStream, mimeType);
            await filesDatabase.InsertFile(fileHash, mimeType, fileStream.Length);
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    /// <summary>
    /// Implementation for local file storage. Not really meant to be used in production scenarios.
    /// </summary>
    public class LocalFilesStorageDatabase : IFilesStorageDatabase
    {
        private static string basePath = Path.Join(Path.GetFullPath(Assembly.GetEntryAssembly().Location), "../Roblox.Files.Services.Storage/");

        static LocalFilesStorageDatabase()
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                Console.WriteLine("Created local files directory at: {0}", basePath);
            }
        }

        private static Regex fileIdRegex = new Regex("[a-z0-9]+");
        
        public static string GetFullPath(string fileId)
        {
            var safeId = fileIdRegex.Match(fileId);
            if (!safeId.Success || safeId.Groups[0].Value != fileId)
                throw new ArgumentException("fileId is Not Safe\nID = " + fileId + "\nExpected = " + safeId?.Groups?[0]?.Value);
            return Path.Join(basePath, "./" + fileId);
        }
        
        public async Task<Stream> GetFileById(string fileId)
        {
            var filePath = GetFullPath(fileId);
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
        }

        public async Task UploadFile(string id, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        public async Task UploadFile(string id, Stream fileStream, string mimeType)
        {
            var filePath = GetFullPath(id);
            await using var destinationFileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096, true);
            while (fileStream.Position < fileStream.Length)
            {
                destinationFileStream.WriteByte((byte)fileStream.ReadByte());
            }
        }

        public async Task DeleteFile(string id)
        {
            var filePath = GetFullPath(id);
            await using (new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096,
                FileOptions.DeleteOnClose))
            {
                // Do nothing so the file is deleted...
                // https://stackoverflow.com/a/46699436/7841868
            }
        }
    }
}
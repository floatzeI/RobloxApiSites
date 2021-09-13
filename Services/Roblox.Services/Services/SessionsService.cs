using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Sessions;

namespace Roblox.Services.Services
{
    public class SessionsService : ISessionsService
    {
        private ISessionsDatabase db { get; set; }

        public SessionsService(ISessionsDatabase db)
        {
            this.db = db;
        }

        public static string sessionCookiePrefix =
            "_|WARNING:-DO-NOT-SHARE-THIS.--Sharing-this-will-allow-someone-to-log-in-as-you-and-to-steal-your-ROBUX-and-items.|_";

        /// <summary>
        /// Array of valid lengths the session identifier can be (excluding prefix). The total string length will be double this.
        /// </summary>
        private int[] allowedSessionLengths = { 324, 356 };

        public string CreateUniqueSessionIdentifier()
        {
            using var randCryptoProvider = new RNGCryptoServiceProvider();
            var index = new Random().Next(allowedSessionLengths.Length);
            var bytes = new byte[allowedSessionLengths[index]];
            randCryptoProvider.GetNonZeroBytes(bytes);
            return string.Join("", bytes.Select(b => b.ToString("X2")));
        }
        
        public async Task<CreateSessionResponse> CreateSession(long userId)
        {
            var uniqueId = CreateUniqueSessionIdentifier();
            var insertResult = await db.InsertSession(userId, uniqueId);
            return new()
            {
                userId = userId,
                sessionId = sessionCookiePrefix + insertResult.id,
            };
        }

        public async Task DeleteSession(string sessionIdWithPrefix)
        {
            var id = sessionIdWithPrefix[sessionCookiePrefix.Length..];
            await db.DeleteSession(id);
        }

        public async Task<SessionEntry> GetSession(string sessionIdWithPrefix)
        {
            var id = sessionIdWithPrefix[sessionCookiePrefix.Length..];
            var result = await db.GetSession(id);
            if (result == null) throw new RecordNotFoundException();
            result.id = sessionCookiePrefix + result.id;
            return result;
        }

        public async Task ReportSessionUsage(string sessionIdWithPrefix)
        {
            var id = sessionIdWithPrefix[sessionCookiePrefix.Length..];
            await db.SetSessionUpdatedAt(id, DateTime.Now);
        }
    }
}
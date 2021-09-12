using Roblox.Services.Database;

namespace Roblox.Services.Services
{
    public class SessionsService : ISessionsService
    {
        private ISessionsDatabase db { get; set; }

        public SessionsService(ISessionsDatabase db)
        {
            this.db = db;
        }

    }
}
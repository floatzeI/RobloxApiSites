using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;

namespace Roblox.Services.Services
{
    public class AvatarService : IAvatarService
    {
        private IAvatarDatabase db { get; set; }

        public AvatarService(IAvatarDatabase db)
        {
            this.db = db;
        }
    }
}
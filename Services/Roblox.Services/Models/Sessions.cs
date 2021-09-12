using System;

namespace Roblox.Services.Models.Sessions
{
    public class SessionEntry
    {
        public string id { get; set; }
        public long userId { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class CreateSessionResponse
    {
        public long userId { get; set; }
        public string sessionId { get; set; }
    }
}
using System;

namespace Roblox.Services.Models.Users
{
    public class UserDescriptionEntry
    {
        public long userId { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
using System;

namespace Roblox.Services.Models.Passwords
{
    public class UserAccountPasswordEntry
    {
        public long userId { get; set; }
        public string passwordHash { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
using System;

namespace Roblox.Administration.Models.Users
{
    public class UserSearchEntry
    {
        public long userId { get; set; }
        public string username { get; set; }
        public DateTime created { get; set; }
    }
}
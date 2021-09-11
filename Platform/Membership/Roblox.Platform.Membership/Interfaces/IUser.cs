using System;

namespace Roblox.Platform.Membership
{
    public class IUser
    {
        public string name { get; set; }
        public long id { get; set; }
        public DateTime created { get; set; }
        public AccountStatus accountStatus { get; set; }
    }
}
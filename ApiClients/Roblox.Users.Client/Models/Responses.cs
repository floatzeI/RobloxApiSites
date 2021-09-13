using System;
using Roblox.Platform.Membership;

namespace Roblox.Users.Client.Models.Responses
{
    public class DescriptionResponse
    {
        public long userId { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class GetUserByIdEntry
    {
        public long userId { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public AccountStatus accountStatus { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime created { get; set; }
    }
}
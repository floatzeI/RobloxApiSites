using System;

namespace Roblox.Users.Client.Models.Responses
{
    public class DescriptionResponse
    {
        public long userId { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
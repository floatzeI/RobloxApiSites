using System;

namespace Roblox.Services.Models.AccountInformation
{
    public class AccountInformationEntry
    {
        public long userId { get; set; }
        public string description { get; set; }
    }

    public class AccountDescriptionEntry
    {
        public long userId { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
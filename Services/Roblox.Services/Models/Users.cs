using System;
using System.ComponentModel.DataAnnotations;

namespace Roblox.Services.Models.Users
{
    public class UserDescriptionEntry
    {
        public long userId { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class SetDescriptionRequest
    {
        [Required]
        public long userId { get; set; }
        [Required]
        public string description { get; set; }
    }

    public class AccountInformationEntry
    {
        public long userId { get; set; }
        public string description { get; set; }
        public int? gender { get; set; }
        public int? birthYear { get; set; }
        public int? birthMonth { get; set; }
        public int? birthDay { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
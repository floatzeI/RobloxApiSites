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

    public class ValidatePasswordRequest
    {
        public long userId { get; set; }
        public string password { get; set; }
    }
    
    public class ValidatePasswordResponse
    {
        public bool isCorrect { get; set; }
    }

    public class SetPasswordRequest
    {
        public string password { get; set; }
        public long userId { get; set; }
    }
}
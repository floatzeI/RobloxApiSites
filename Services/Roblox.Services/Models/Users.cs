using System;
using System.ComponentModel.DataAnnotations;
using Roblox.Platform.Membership;

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

    public class UserInformationResponse
    {
        public long userId { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public AccountStatus accountStatus { get; set; }
        public DateTime birthdate { get; set; }
        public DateTime created { get; set; }
    }

    public class UserAccountEntry
    {
        public long userId { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public AccountStatus accountStatus { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class SkinnyUserAccountEntry
    {
        public long userId { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
    }

    public class CreateUserRequest
    {
        public string username { get; set; }
        public int birthYear { get; set; }
        public int birthMonth { get; set; }
        public int birthDay { get; set; }
        /// <summary>
        /// Get whether the request is valid
        /// </summary>
        /// <returns>Returns the invalid parameter as a string, or null if everything is OK</returns>
        public string Validate()
        {
            // validate birth
            var birthCheck = Validators.Users.ValidateBirthDate(birthYear, birthMonth, birthDay);
            if (birthCheck != null) return birthCheck;

            // validate name
            if (string.IsNullOrEmpty(username) || username.Length is > 20 or < 3)
            {
                return "username";
            }
            return null; // OK
        }
    }
    
    public class SetBirthDateRequest
    {
        public long userId { get; set; }
        public int birthYear { get; set; }
        public int birthMonth { get; set; }
        public int birthDay { get; set; }
        public string Validate()
        {
            // validate birth
            var birthCheck = Validators.Users.ValidateBirthDate(birthYear, birthMonth, birthDay);
            if (birthCheck != null) return birthCheck;

            // validate id
            if (userId <= 0) return "username";
            return null; // OK
        }
    }

    public class SetGenderRequest
    {
        public long userId { get; set; }
        public byte gender { get; set; }
    }
}
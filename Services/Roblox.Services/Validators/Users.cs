using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Roblox.Services.Validators
{
    public static class Users
    {
        /// <summary>
        /// Confirm a birth date is valid
        /// </summary>
        /// <param name="birthYear"></param>
        /// <param name="birthMonth"></param>
        /// <param name="birthDay"></param>
        /// <returns>Null if valid, or the parameter that is bad if invalid</returns>
        public static string ValidateBirthDate(int birthYear, int birthMonth, int birthDay)
        {
            var maxYear = DateTime.Now.Year;
            var minYear = maxYear - 100;
            if (birthYear > maxYear || birthYear < minYear)
            {
                return "birthYear";
            }

            if (birthMonth is < 1 or > 12)
            {
                return "birthMonth";
            }

            if (birthDay is < 1 or > 31)
            {
                return "birthDay";
            }
            try
            {
                var data = new DateTime(birthYear, birthMonth, birthDay);
                if (data.Year != birthYear) return "birthYear";
                if (data.Month != birthMonth) return "birthMonth";
                if (data.Day != birthDay) return "birthDay";
                return null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return "birthDay";
            }
        }

        private static readonly Regex _allowedUsernameCharacters = new Regex("[a-zA-Z0-9_]+");

        private static readonly string[] _specialCharacters =
        {
            "_"
        };
        
        /// <summary>
        /// Confirm the username is valid
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>"username" on validation error, null otherwise</returns>
        public static string ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return "username"; // must not be null
            if (username.Length is < 3 or > 20) return "username"; // must be 3-20 characters (inclusive)
            var expectedName = _allowedUsernameCharacters.Match(username);
            if (expectedName.Groups.Count == 0 || expectedName.Groups[0].Value != username) return "username"; // Username contains invalid characters
            if (_specialCharacters.Contains(username.Substring(0, 1))) return "username"; // cannot start with forbidden characters
            if (_specialCharacters.Contains(username.Substring(username.Length - 1))) return "username"; // cannot end with forbidden characters
            foreach (var item in _specialCharacters)
            {
                var firstIdx = username.IndexOf(item, StringComparison.Ordinal);
                if (firstIdx < 0) continue;
                var secondIdx = username.IndexOf(item, firstIdx, StringComparison.Ordinal);
                if (secondIdx >= 0)
                {
                    return "username"; // username contains multiple instances of item
                }
            }
            return null; // OK
        }
    }
}
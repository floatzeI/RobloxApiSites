using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Roblox.Authentication.Api.Models;

namespace Roblox.Authentication.Api.Validators
{
    internal static class PasswordValidator
    {
        private static string GetMessageForStatus(PasswordValidationStatus status)
        {
            return status switch
            {
                PasswordValidationStatus.ValidPassword => "Password is valid",
                PasswordValidationStatus.DumbStringsError => "This password is not allowed. Please choose a different password",
                PasswordValidationStatus.ShortPasswordError => "Password must be at least 8 characters long",
                PasswordValidationStatus.PasswordSameAsUsernameError => "Password must not be your username",
                PasswordValidationStatus.ForbiddenPasswordError => "ForbiddenPasswordError", // todo: what is the error message for this?
                PasswordValidationStatus.WeakPasswordError => "Please create a more complex password",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        private static List<string> dumbPasswordsList = new List<string>
        {
            // todo: this should use the top 10k passwords list, in addition to some Roblox-specific phrases
            "roblox",
            "roblox123",
            "password123",
        };

        private static Regex numberRegex = new Regex("[0-9]+");
        private static Regex lettersRegex = new("[a-zA-Z]+");
        
        private static Models.PasswordValidationStatus GetStatusForPassword(string passwordToCheck)
        {
            if (passwordToCheck.Length < 8) return PasswordValidationStatus.ShortPasswordError;
            if (dumbPasswordsList.Contains(passwordToCheck.ToLower())) return PasswordValidationStatus.DumbStringsError;
            // check if password is just numbers
            var numberMatches = numberRegex.Match(passwordToCheck);
            if (numberMatches.Groups.Count > 0 && numberMatches.Groups[0].Value == passwordToCheck)
                return PasswordValidationStatus.WeakPasswordError;
            // check if password is just letters
            var letterMatches = lettersRegex.Match(passwordToCheck);
            if (letterMatches.Groups.Count > 0 && letterMatches.Groups[0].Value == passwordToCheck)
                return PasswordValidationStatus.WeakPasswordError;
            // password seems ok
            return PasswordValidationStatus.ValidPassword;
        }
        
        public static Models.PasswordValidationResponse GetPasswordStatus(string username, string passwordToCheck)
        {
            var status = GetStatusForPassword(passwordToCheck);
            if (username == passwordToCheck)
            {
                status = PasswordValidationStatus.PasswordSameAsUsernameError;
            }
            var msg = GetMessageForStatus(status);
            return new()
            {
                code = status,
                message = msg,
            };
        }
    }
}
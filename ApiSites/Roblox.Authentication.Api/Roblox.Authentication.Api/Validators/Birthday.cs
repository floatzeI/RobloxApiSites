using System;

namespace Roblox.Authentication.Api.Validators
{
    public static class BirthdayValidator
    {
        /// <summary>
        /// Validate a birthday
        /// </summary>
        /// <returns>Null if valid, otherwise the faulting segment</returns>
        public static string ValidateBirthday(DateTime birthDate)
        {
            var maxYear = DateTime.Now.Year - 100;
            var minYear = DateTime.Now.Year;
            if (birthDate.Year > minYear || birthDate.Year < maxYear)
            {
                return "year";
            }

            return null;
        }
    }
}
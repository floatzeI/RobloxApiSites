namespace Roblox.Authentication.Api.Models
{
    public enum PasswordValidationStatus
    {
        ValidPassword = 0,
        ShortPasswordError = 2,
        ForbiddenPasswordError,
        /// <summary>
        /// Password is too simple (e.g. in top 10k most common passwords, or "Roblox", or something)
        /// </summary>
        DumbStringsError,
        WeakPasswordError,
        PasswordSameAsUsernameError,
    }
}
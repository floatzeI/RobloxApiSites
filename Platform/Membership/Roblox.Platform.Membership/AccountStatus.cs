namespace Roblox.Platform.Membership
{
    public enum AccountStatus
    {
        /// <summary>
        /// Account is normal
        /// </summary>
        Ok = 1,
        /// <summary>
        /// Temporary ban, hidden from search
        /// </summary>
        Suppressed,
        /// <summary>
        /// Permanently banned (aka terminated)
        /// </summary>
        Deleted,
        /// <summary>
        /// Same as <see cref="AccountStatus.Deleted"/> as far as I can tell; not used anymore aside from accounts already with this status
        /// </summary>
        Poisoned,
        /// <summary>
        /// Locked account
        /// </summary>
        MustValidateEmail,
        /// <summary>
        /// GPDR Deleted (essentially terminated but user must be 100% hidden from all endpoints - as if they don't exist!)
        /// </summary>
        Forgotten,
    }
}
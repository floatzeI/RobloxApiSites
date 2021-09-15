using System;

namespace Roblox.Avatar.Client.Exceptions
{
    public class UserHasNoAvatarException : Exception
    {
        public UserHasNoAvatarException(long userId, Exception inner) : base("User has no avatar.\nID = " + userId, inner)
        {
            
        }
    }
}
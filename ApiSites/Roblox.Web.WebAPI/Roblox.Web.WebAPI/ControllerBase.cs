using Roblox.Platform.Membership;

namespace Roblox.Web.WebAPI.Controllers
{
    public class ApiControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public IUser _userOverrideForTests { get; set; }
        
        public IUser authenticatedUser
        {
            get => _userOverrideForTests ?? (IUser)HttpContext.Items["AuthenticatedUser"];
        }
    }
}
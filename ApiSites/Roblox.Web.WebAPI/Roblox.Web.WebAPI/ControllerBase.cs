using Roblox.Platform.Membership;

namespace Roblox.Web.WebAPI.Controllers
{
    public class ApiControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public IUser authenticatedUser => (IUser)HttpContext.Items["AuthenticatedUser"];
    }
}
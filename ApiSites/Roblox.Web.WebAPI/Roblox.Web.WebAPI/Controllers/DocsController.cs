using Microsoft.AspNetCore.Mvc;

namespace Roblox.Web.WebAPI.Controllers
{
    [Controller]
    [Route("/docs")]
    public class DocsController : ApiControllerBase
    {
        [HttpGet("metadata")]
        public DocMetadataModel GetMetadata()
        {
            return new()
            {
                title = Pages.Docs.pageTitle,
                description = Pages.Docs.pageDescription,
                versions = Pages.Docs.versions,
            };
        }
    }
}
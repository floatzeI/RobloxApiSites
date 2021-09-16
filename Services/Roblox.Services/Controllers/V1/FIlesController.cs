using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Files/v1")]
    public class FilesController
    {
        private IFilesService filesService { get; set; }

        public FilesController(IFilesService service)
        {
            filesService = service;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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

        [HttpPost("UploadFile")]
        public async Task<Models.Files.UploadResponse> UploadFile(
            [FromBody, Required] Models.Files.UploadRequest request)
        {
            var stream = request.file.OpenReadStream();
            var hash = await filesService.CreateFileHash(stream);
            
            await filesService.UploadFile(stream, hash, request.mime ?? request.file.ContentType);
            
            return new()
            {
                id = hash,
            };
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Thumbnails/v1")]
    public class ThumbnailsController
    {
        private IThumbnailsService thumbnailsService { get; set; }

        public ThumbnailsController(IThumbnailsService service)
        {
            thumbnailsService = service;
        }
        
        [HttpGet("GetThumbnail")]
        public async Task<Models.Thumbnails.ThumbnailEntry> GetThumbnail([Required] long referenceId, [Required] int thumbnailType,
            [Required] int resolutionX, [Required] int resolutionY)
        {
            return await thumbnailsService.GetThumbnail(referenceId, thumbnailType, resolutionX, resolutionY);
        }

        [HttpPost("InsertThumbnail")]
        public async Task InsertThumbnail([Required, FromBody] Models.Thumbnails.ThumbnailEntry request)
        {
            await thumbnailsService.InsertThumbnail(request);
        }

        [HttpPost("DeleteThumbnailsForReference")]
        public async Task DeleteThumbnailsForReference([Required] long referenceId, [Required] int thumbnailType)
        {
            await thumbnailsService.DeleteThumbnailsForReference(referenceId, thumbnailType);
        }

        [HttpGet("GetThumbnailByHash")]
        public async Task<Models.Thumbnails.ThumbnailEntry> GetThumbnailByHash([Required] string thumbnailHash,
            [Required] int resolutionX, [Required] int resolutionY)
        {
            return await thumbnailsService.GetThumbnailByHash(thumbnailHash, resolutionX, resolutionY);
        }
    }
}
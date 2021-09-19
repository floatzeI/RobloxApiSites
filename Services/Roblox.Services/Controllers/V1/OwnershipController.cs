using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Ownership/v1")]
    public class OwnershipController
    {
        private IOwnershipService ownershipService { get; set; }

        public OwnershipController(IOwnershipService service)
        {
            ownershipService = service;
        }

        [HttpPost("InsertEntry")]
        public async Task<Models.Ownership.OwnershipEntry> InsertEntry(
            [Required, FromBody] Models.Ownership.CreateRequest request)
        {
            return await ownershipService.InsertEntry(request);
        }

        [HttpGet("AgentOwnsAsset")]
        public async Task<Models.Ownership.OwnershipResponse> AgentOwnsAsset([Required] long agentId,
            [Required] long assetId)
        {
            var status = await ownershipService.DoesUserOwnsAsset(agentId, assetId);
            return new()
            {
                isOwner = status,
            };
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Roblox.ApiProxy.Api.Controllers
{
    [ApiController]
    [Route("/Marketplace")]
    public class MarketplaceController
    {
        [HttpGet("GetProductInfo")]
        public async Task<Models.ProductInfoResponse> GetProductInfo([FromQuery] long assetId)
        {
            throw new NotImplementedException();
        }
    }
}
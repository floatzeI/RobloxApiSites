using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Lib.Extensions;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
{
    [ApiController]
    [Route("/Assets/v1")]
    public class AssetsController
    {
        private IAssetsService assetsService { get; set; }

        public AssetsController(IAssetsService service)
        {
            assetsService = service;
        }
        
        [HttpGet("MultiGetDetails")]
        public async Task<IEnumerable<Models.Assets.AssetEntry>> MultiGetAssetsById(string assetIds)
        {
            return await assetsService.MultiGetAssetsById(ListExtensions.CsvToLongList(assetIds));
        }

        [HttpGet("GetDetails")]
        public async Task<Models.Assets.AssetEntry> GetAssetById([Required] long assetId)
        {
            return await assetsService.GetAssetById(assetId);
        }

        [HttpPost("InsertAsset")]
        public async Task<Models.Assets.AssetEntry> InsertAsset([Required, FromBody] Models.Assets.InsertAssetRequest request)
        {
            return await assetsService.InsertAsset(request);
        }

        [HttpPost("UpdateAsset")]
        public async Task UpdateAsset([Required, FromBody] Models.Assets.UpdateAssetRequest request)
        {
            await assetsService.UpdateAsset(request);
        }

        [HttpGet("GetAssetGenres")]
        public async Task<IEnumerable<int>> GetAssetGenres([Required] long assetId)
        {
            return await assetsService.GetAssetGenres(assetId);
        }
        
        [HttpPost("SetAssetGenres")]
        public async Task SetAssetGenres([Required, FromBody] Models.Assets.UpdateAssetGenresRequest request)
        {
            await assetsService.SetAssetGenres(request.assetId, request.genres);
        }
    }
}
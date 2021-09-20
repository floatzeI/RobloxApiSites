using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roblox.Platform.Asset;
using Roblox.Web.Enums;

namespace Roblox.Administration.Pages.Assets
{
    public class Create : PageModel
    {
        private IAssetManager assetManager { get; set; }
        public Create(IAssetManager assetManagerParam)
        {
            assetManager = assetManagerParam;
        }
        public string okMessage { get; set; }
        public string okUrl { get; set; }
        public string errorMessage { get; set; }
        
        public void OnGet()
        {
            
        }
        
        [BindProperty, Required, MinLength(3), MaxLength(64)]
        public string name { get; set; }
        [BindProperty]
        public string description { get; set; }
        [BindProperty, Required]
        public IFormFile rbxFile { get; set; }
        [BindProperty]
        public AssetType assetType { get; set; }
        [BindProperty]
        public bool isForSale { get; set; }
        [BindProperty]
        public int? priceInRobux { get; set; }
        [BindProperty]
        public int? priceInTickets { get; set; }
        [BindProperty]
        public string limitedStatus { get; set; }
        [BindProperty]
        public int? copyCount { get; set; }

        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                errorMessage = string.Join("\n", allErrors.Select(c => c.ErrorMessage));
                return;
            }
            // reset
            if (copyCount <= 0)
            {
                copyCount = null;
            }

            if (priceInRobux <= 0)
            {
                priceInRobux = null;
            }

            if (priceInTickets <= 0)
            {
                priceInTickets = null;
            }

            var details = await assetManager.CreateAsset(new()
            {
                assetType = assetType,
                creatorId = 1,
                creatorType = CreatorType.User,
                description = description,
                economyInfo = new()
                {
                    isForSale = isForSale,
                    isLimited = limitedStatus == "isLimited",
                    isLimitedUnique = limitedStatus == "isLimitedUnique",
                    offSaleDeadline = null,
                    priceInRobux = priceInRobux,
                    priceInTickets = priceInTickets,
                    stockCount = limitedStatus == "isLimitedUnique" ? copyCount : null,
                },
                file = rbxFile.OpenReadStream(),
                name = name,
                userId = 1,
                genres = new List<AssetGenre>(){},
            });

            okMessage = "Asset was created. ID=" + details.assetId + " UAID=" + details.userAssetId + " AVID=" + details.assetVersionId + " PID=" + details.productId;
            okUrl = "https://www.roblox.com/catalog/"+details.assetId+"/--";
        }
    }
}
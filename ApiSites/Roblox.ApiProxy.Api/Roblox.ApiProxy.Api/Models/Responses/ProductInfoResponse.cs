using System;
using Roblox.Web.Enums;

namespace Roblox.ApiProxy.Api.Models
{
    public class ProductInfoResponse
    {
        public long TargetId { get; set; }
        public string ProductType { get; set; }
        public long AssetId { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetType AssetTypeId { get; set; }
        public Creator Creator { get; set; }
        public long IconImageAssetId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public int? PriceInRobux { get; set; }
        public int? PriceInTickets { get; set; }
        public int Sales { get; set; }
        public bool IsNew { get; set; }
        public bool IsForSale { get; set; }
        public bool IsPublicDomain { get; set; }
        public bool IsLimited { get; set; }
        public bool IsLimitedUnique { get; set; }
        public int? Remaining { get; set; }
        public int MinimumMembershipLevel { get; set; }
        public int ContentRatingTypeId { get; set; }
    }
}
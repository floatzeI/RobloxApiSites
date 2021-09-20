using Roblox.Web.Enums;

namespace Roblox.ApiProxy.Api.Models
{
    public class Creator
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CreatorType CreatorType { get; set; }
        public long CreatorTargetId { get; set; }
    }
}
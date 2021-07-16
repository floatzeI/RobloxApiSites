namespace Roblox.AccountInformation.Api.Models
{
    public class MetadataResponse
    {
        public bool isAllowedNotificationsEndpointDisabled { get; set; }
        public bool isAccountSettingsPolicyEnabled { get; set; }
        public bool isPhoneNumberEnabled { get; set; }
        public int MaxUserDescriptionLength { get; set; }
        public bool isUserDescriptionEnabled { get; set; }
        public bool isUserBlockEndpointsUpdated { get; set; }
    }
}

namespace Roblox.AccountInformation.Api.Models
{
    public class BirthdayResponse
    {
        /// <summary>The birth month</summary>
        public int birthMonth { get; set; }
        /// <summary>The birth day</summary>
        public int birthDay { get; set; }
        /// <summary>The birth year</summary>
        public int birthYear { get; set; }
    }

    public class BirthdayRequest : BirthdayResponse { }

    public class DescriptionResponse
    {
        /// <summary>The description</summary>
        public string description { get; set; }
    }

    public class DescriptionRequest : DescriptionResponse { }

    public class GenderResponse
    {
        /// <summary>The gender</summary>
        public int gender { get; set; }
    }

    public class GenderRequest : GenderResponse { }

    public class ConsecutiveLoginDaysResponse
    {
        /// <summary>Consecutive login days</summary>
        public int count { get; set; }
    }
}
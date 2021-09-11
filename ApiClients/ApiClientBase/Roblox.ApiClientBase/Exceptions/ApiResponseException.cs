namespace Roblox.ApiClientBase
{
    public class ApiResponseException : System.Exception
    {
        public ApiClientResponse response { get; }
        public ApiResponseException(ApiClientResponse response) : base(" The remote server returned an error: ("+(int)response.statusCode+") "+ response.statusCode + ".")
        {
            this.response = response;
        }
    }
}
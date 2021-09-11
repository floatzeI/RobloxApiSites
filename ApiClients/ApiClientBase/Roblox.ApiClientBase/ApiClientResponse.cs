using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace Roblox.ApiClientBase
{
    public class ApiClientResponse
    {
        public string machineId { get; set; }
        public string url { get; set; }
        public string body { get; set; }
        public HttpResponseHeaders headers { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
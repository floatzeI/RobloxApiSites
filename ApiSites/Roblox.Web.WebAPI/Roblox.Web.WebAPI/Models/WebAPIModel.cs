using System.Collections;
using System.Collections.Generic;

namespace Roblox.Web.WebAPI
{
    public class ApiEmptyResponseModel { }

    public class ErrorResponse
    {
        public IEnumerable<ErrorEntry> errors { get; set; }
    }

    public class ErrorEntry
    {
        public int code { get; set; }
        public string message { get; set; }
        public string userFacingMessage { get; set; }
    }
}
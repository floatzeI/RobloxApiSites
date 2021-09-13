using System.Collections;
using System.Collections.Generic;

namespace Roblox.ApiClientBase.Models
{
    public class ApiErrorResponse
    {
        public IEnumerable<ApiErrorEntry> errors { get; set; }
    }

    public class ApiErrorEntry
    {
        public int code { get; set; }
        public string codeDescription { get; set; }
        public string message { get; set; }
    }
}
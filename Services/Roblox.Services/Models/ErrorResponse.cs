using System.Collections;
using System.Collections.Generic;

namespace Roblox.Services.Models
{
    public class ErrorResponseEntry
    {
        /// <summary>
        /// The <see cref="ErrorCode" />
        /// </summary>
        public ErrorCode code { get; set; }
        /// <summary>
        /// The <see cref="ErrorCode" /> serialized as string
        /// </summary>
        public string codeDescription => code.ToString();
        /// <summary>
        /// This will include a stack trace in development
        /// </summary>
        public string message { get; set; }
    }
    
    public class ErrorResponse
    {
        public IEnumerable<ErrorResponseEntry> errors { get; set; }
    }
}
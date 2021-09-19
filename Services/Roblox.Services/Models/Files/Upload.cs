using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Roblox.Services.Models.Files
{
    public class UploadRequest
    {
        /// <summary>
        /// The file mime type. Defaults to null.
        /// </summary>
        public string mime { get; set; }
        /// <summary>
        /// The file to upload
        /// </summary>
        public IFormFile file { get; set; }
    }

    public class UploadResponse
    {
        /// <summary>
        /// The generated fileId. This should be a hash, up to 64 characters.
        /// </summary>
        public string id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Roblox.Administration.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string requestId { get; set; }

        public bool showRequestId => !string.IsNullOrEmpty(requestId);

        public HttpStatusCode? statusCode { get; set; } = null;

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet([FromQuery] HttpStatusCode? status)
        {
            if (status != null)
            {
                statusCode = (HttpStatusCode)status;
            }
            requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roblox.Administration.Models.Users;

namespace Roblox.Administration.Pages.Users
{
    public class Lookup : PageModel
    {
        public IEnumerable<Models.Users.UserSearchEntry> searchResults { get; set; }
        public int searchTotal { get; set; }
        public string searchField { get; set; }
        public string searchQuery { get; set; }
        
        public async Task OnGetAsync([FromQuery] string username, [FromQuery] long? userId, [FromQuery] string emailAddress)
        {
            if (username != null)
            {
                searchField = "Username";
                searchQuery = username;
                searchTotal = 1;
                searchResults = new UserSearchEntry[]
                {
                    new()
                    {
                        username = "Test",
                        userId = 1,
                        created = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                    },
                };
            }
        }
    }
}
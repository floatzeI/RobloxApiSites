using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roblox.Web.WebAPI.Pages
{
    public class Docs : PageModel
    {
        public static string pageTitle { get; set; }
        public static string pageDescription { get; set; }
        public static string[] versions { get; set; }
        
        public void OnGet()
        {
            
        }
    }
}
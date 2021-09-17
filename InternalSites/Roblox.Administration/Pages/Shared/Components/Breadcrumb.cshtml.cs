using System.Collections.Generic;
using Roblox.Administration.Models.Partials;

namespace Roblox.Administration.Partials
{
    public class BreadcrumbsModel
    {
        public List<BreadcrumbEntry> pageSections { get; set; } = new();
        public BreadcrumbEntry currentPage => pageSections[^1];

        public static BreadcrumbsModel FromTitle(string pageTitle)
        {
            var split = pageTitle.Split(">");
            var m = new BreadcrumbsModel();
            var previous = "";
            foreach (var unprocessedItem in split)
            {
                var item = unprocessedItem.Trim();
                previous += "/" + item;
                m.pageSections.Add(new BreadcrumbEntry()
                {
                    name = item,
                    url = previous,
                });
            }
            return m;
        }
    }
}
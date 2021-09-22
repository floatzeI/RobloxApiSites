using System.Collections.Generic;

namespace Roblox.Rendering.Client.Models
{
    public class RenderRequest
    {
        public string command { get; set; }
        public List<dynamic> args { get; set; }
        public string id { get; set; }
    }
}
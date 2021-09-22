using System;
using System.Collections.Generic;

namespace Roblox.Rendering.Client.Models
{
    public class RenderPoolEntry
    {
        public string command { get; set; }
        public string id { get; set; }
        public List<dynamic> parameters { get; set; }
        public Func<RenderResponse, string, object> onFinish { get; set; }
    }
}
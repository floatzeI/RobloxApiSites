using System;
using Microsoft.VisualBasic.CompilerServices;

namespace Roblox.Sessions.Client.Models.Responses
{
    public class GetSessionByIdResponse
    {
        public long userId { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}
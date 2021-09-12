using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Models;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Sessions/v1")]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public class SessionsController
    {
        private ISessionsService sessionService { get; }
        public SessionsController(Services.ISessionsService service)
        {
            sessionService = service;
        }

        [HttpPost("CreateSessionForAccount")]
        public async Task<CreateSessionResponse> CreateSession([Required] long userId)
        {
            return await sessionService.CreateSession(userId);
        }

        [HttpPost("DeleteSessionForAccount")]
        public async Task DeleteSession([Required] string sessionId)
        {
            await sessionService.DeleteSession(sessionId);
        }
    }
}
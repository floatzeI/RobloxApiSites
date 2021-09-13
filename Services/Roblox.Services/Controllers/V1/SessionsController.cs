using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Models;
using Roblox.Services.Models.Sessions;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers.V1
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
        public async Task DeleteSession([Required, MinLength(500)] string sessionId)
        {
            await sessionService.DeleteSession(sessionId);
        }

        /// <summary>
        /// Get a session by its ID (aka the cookie value)
        /// </summary>
        /// <param name="sessionId">The sessionId, with prefix</param>
        /// <returns>The <see cref="SessionEntry"/> corresponding to this id</returns>
        /// <response code="400">
        /// The session does not exist
        /// </response>
        [HttpGet("GetSessionById")]
        public async Task<SessionEntry> GetSession([Required, MinLength(500)] string sessionId)
        {
            return await sessionService.GetSession(sessionId);
        }

        /// <summary>
        /// Update the "updated_at" value for the sessionId
        /// </summary>
        /// <param name="sessionId">The id of the session, with prefix</param>
        [HttpGet("ReportSessionUsage")]
        public async Task ReportSessionUsage([Required, MinLength(500)] string sessionId)
        {
            await sessionService.ReportSessionUsage(sessionId);
        }
    }
}
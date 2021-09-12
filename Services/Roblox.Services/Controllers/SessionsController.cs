using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roblox.Services.Models;
using Roblox.Services.Services;

namespace Roblox.Services.Controllers
{
    [ApiController]
    [Route("/Users/v1")]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public class SessionsController
    {
        private IUsersService usersService { get; }
        public SessionsController(Services.IUsersService service)
        {
            usersService = service;
        }
        
    }
}
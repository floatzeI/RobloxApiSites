using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Roblox.Authentication.Api.Models
{
    public class PasswordValidationRequest
    {
        /// <summary>
        /// The username.
        /// </summary>
        [Required, FromQuery()]
        public string username { get; set; }
        /// <summary>
        /// The password.
        /// </summary>
        [Required, FromQuery()]
        public string password { get; set; }
    }
}
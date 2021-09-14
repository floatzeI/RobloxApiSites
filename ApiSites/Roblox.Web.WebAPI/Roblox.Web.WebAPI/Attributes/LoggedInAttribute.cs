using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Roblox.Platform.Membership;
using Roblox.Sessions.Client;
using Roblox.Sessions.Client.Exceptions;
using Roblox.Users.Client;

namespace Roblox.Web.WebAPI
{
    public class LoggedInAttribute : ActionFilterAttribute
    {
        private static ISessionsV1Client sessionsClient { get; set; }
        private static IUsersV1Client usersClient { get; set; }

        public static void SetClients(ISessionsV1Client newSessionsClient, IUsersV1Client newUsersClient)
        {
            sessionsClient = newSessionsClient;
            usersClient = newUsersClient;
        }
        
        private void OnAuthenticationFail(ActionExecutingContext ctx)
        {
            ctx.Result = new ObjectResult(new
            {
                errors = new List<dynamic>()
                {
                    new
                    {
                        code = 0,
                        message = "Authorization has been denied for this request.",
                    }
                }
            })
            {
                StatusCode = 401,
            };
        }

        private async Task<IUser> GetIUserFromCookie(string cookieValue)
        {
            var result = await sessionsClient.GetSessionById(cookieValue);
            var userInfo = await usersClient.GetUserById(result.userId);
            return new()
            {
                id = userInfo.userId,
                name = userInfo.username,
                accountStatus = userInfo.accountStatus,
                created = userInfo.created,
            };
        }

        /// <summary>
        /// Get the cookie from http context, or null if not exists/not specified
        /// </summary>
        /// <param name="ctx">The <see cref="HttpContext"/></param>
        /// <returns>The cookie string, or null if does not exist</returns>
        private string GetCookieFromContext(HttpContext ctx)
        {
            var cookieName = ".ROBLOSECURITY";
            var cookieExists = ctx.Request.Cookies.ContainsKey(cookieName);
            if (!cookieExists)
            {
                return null;
            }

            var value = ctx.Request.Cookies[cookieName];
            if (string.IsNullOrEmpty(value) || value.Length is < 500 or > 1024)
            {
                return null;
            }

            return value;
        }
        
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ctx = context.HttpContext;
            var value = GetCookieFromContext(ctx);
            if (value == null)
            {
                OnAuthenticationFail(context);
                return;
            }

            IUser userData;
            try
            {
                userData = await GetIUserFromCookie(value);
            }
            catch (InvalidSessionIdException)
            {
                OnAuthenticationFail(context);
                return;
            }
            
            if (userData.accountStatus is AccountStatus.Deleted or AccountStatus.Poisoned or AccountStatus.Forgotten)
            {
                // todo: return ban response (401 "Unauthorized"), unless url is allowed (e.g. logout)
            }

            if (userData.accountStatus is AccountStatus.MustValidateEmail)
            {
                // todo: delete session, then OnAuthenticationFail(). When user logs in again, they will be forced to reset their password.
            }
            ctx.Items.Add("AuthenticatedUser", userData);

            await next();
        }
    }
}
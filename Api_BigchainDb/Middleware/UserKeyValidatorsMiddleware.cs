using Api_BigchainDb.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BigchainDb.Middleware
{
    public class UserKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        private IContactsRepository ContactsRepo { get; set; }

        public UserKeyValidatorsMiddleware(RequestDelegate next, IContactsRepository _repo)
        {
            _next = next;
            ContactsRepo = _repo;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("user-key") ||
                !ContactsRepo.CheckValidUserKey(context.Request.Headers["user-key"]))
            {
                context.Response.StatusCode = 403;//Forbidden Request                
                await context.Response.WriteAsync("User Key is missing or invalid");
                return;
            }

            #region Check Null Key && Check Invalid Key
            //if (!context.Request.Headers.Keys.Contains("user-key"))
            //{
            //    context.Response.StatusCode = 400; //Bad Request                
            //    await context.Response.WriteAsync("User Key is missing");
            //    return;
            //}
            //else
            //{
            //    if (!ContactsRepo.CheckValidUserKey(context.Request.Headers["user-key"]))
            //    {
            //        context.Response.StatusCode = 401; //UnAuthorized
            //        await context.Response.WriteAsync("Invalid User Key");
            //        return;
            //    }
            //}
            #endregion

            await _next.Invoke(context);
        }
    }
}

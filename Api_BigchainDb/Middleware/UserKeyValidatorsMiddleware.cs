using Api_BigchainDb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api_BigchainDb.Middleware
{
    public class UserKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        private IContactsRepository ContactsRepo { get; set; }
        private readonly ILogger _logger;
        public UserKeyValidatorsMiddleware(RequestDelegate next, IContactsRepository _repo, ILogger<UserKeyValidatorsMiddleware> logger)
        {
            _next = next;
            ContactsRepo = _repo;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("user-key") ||
                !ContactsRepo.CheckValidUserKey(context.Request.Headers["user-key"]))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;             
                await context.Response.WriteAsync("User Key is missing or invalid");
                _logger.LogInformation("Request:" + context.Request.Path + "Method: " + context.Response.StatusCode);

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
            //_logger.LogInformation("Request:" + context.Request.Path + "Method: " + context.Response.StatusCode);

            await _next.Invoke(context);
        }
    }
}

using APITest.Service.Interface;
using System.Net;

namespace APITest.Middleware
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenManager _tokenManager;
        public TokenManagerMiddleware(ITokenManager tokenManager) 
        { 
            _tokenManager = tokenManager;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(!await _tokenManager.isCurrentTokenInDeactivatedList())
            {
                await next(context);
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}

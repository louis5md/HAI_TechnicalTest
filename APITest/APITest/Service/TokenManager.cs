using APITest.Service.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;

namespace APITest.Service
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenManager(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor) 
        {
            cache = distributedCache;
            httpContextAccessor = contextAccessor;
        }


        public async Task DeactivateToken(string token)
            => await cache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                });

        public async Task<bool> isInDeactivatedListToken(string token) 
            => await cache.GetStringAsync(GetKey(token)) != null;
      

        private static string GetKey(string token)
            => $"token:{token}:deactivated";

        private string GetCurrentTokenAsync()
        {
            var authorizationHeader = httpContextAccessor
                .HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty ?
                string.Empty : authorizationHeader;
        }
        public async Task<bool> isCurrentTokenInDeactivatedList()
            => await isInDeactivatedListToken(GetCurrentTokenAsync());
        

        public async Task DeactivateCurrentToken()
            => await DeactivateToken(GetCurrentTokenAsync());
        

  
    }
}

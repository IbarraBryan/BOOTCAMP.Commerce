using Microsoft.AspNetCore.Http;

namespace Api.Gateway.Proxies.Config
{
    public static class HtttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient _httpClient, IHttpContextAccessor _context)
        {
            if(_context.HttpContext.User.Identity.IsAuthenticated && _context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token  = _context.HttpContext.Request.Headers["Authorization"].ToString();
                if(!string.IsNullOrEmpty(token) )
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                }
            }
        }
    }
}

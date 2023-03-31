using Clients.WebClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Clients.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _authenticationUrl;

        public AccountController(IConfiguration configuration)
        {
            _authenticationUrl = configuration.GetValue<string>("AuthenticationUrl");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Redirect($"{_authenticationUrl}?ReturnBaseUrl={this.Request.Scheme}://{this.Request.Host}/");
        }

        [HttpGet]
        public async Task<IActionResult> Connect(string access_token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(access_token);
            var claims = jwtSecurityToken.Claims.ToList();
            claims.Add(new Claim("access_token", access_token));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var auhtProperties = new AuthenticationProperties { IssuedUtc = DateTime.UtcNow.AddHours(8)};

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), auhtProperties);

            return Redirect("~/");
        }
    }
}

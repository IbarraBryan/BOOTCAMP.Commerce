using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.WebClient.Proxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clients.WebClient.Pages.Orders
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetailModel : PageModel
    {
        private readonly ILogger<DetailModel> _logger;
        private readonly IOrderProxy _orderProxy;

        public OrderDto Order { get; set; }

        public DetailModel(
            ILogger<DetailModel> logger,
            IOrderProxy orderProxy
        )
        {
            _orderProxy = orderProxy;
            _logger = logger;
        }

        public async Task OnGet(int id)
        {
            Order = await _orderProxy.GetAsync(id);
        }
    }
}

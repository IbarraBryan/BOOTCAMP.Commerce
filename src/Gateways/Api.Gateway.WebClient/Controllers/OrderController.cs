using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProxy _orderProxy;
        private readonly ICustomerProxy _customerProxy;
        private readonly ICatalogProxy _catalogProxy;

        public OrderController(
            IOrderProxy orderProxy, 
            ICustomerProxy customerProxy,
            ICatalogProxy catalogProxy)
        {
            _orderProxy = orderProxy;
            _customerProxy = customerProxy;
            _catalogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page, int take) => await _orderProxy.GetPagedAsync(page, take);

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var _order = await _orderProxy.GetAsync(id);
            _order.Client = await _customerProxy.GetAsync(_order.ClientId);

            foreach(var item in _order.Items)
                item.Product = await _catalogProxy.GetAsync(item.ProductId);

            return _order;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateAsync(command);
            return Ok();
        }
    }
}

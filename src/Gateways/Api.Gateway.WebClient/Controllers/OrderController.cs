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
        /// <summary>
        /// Este método no necesita traer la información de los productos porque lo usaremos para solo mostrar
        /// las cabeceras en el listado. RECUERDA: que este API Gateway alimenta a nuestro Web.Client - El backend de nuestro frontend
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page, int take)
        {
            var result = await _orderProxy.GetPagedAsync(page, take);
            if (result.HasItems)
            {
                // Retrieve client ids
                var clientIds = result.Items
                    .Select(x => x.ClientId)
                    .GroupBy(g => g)
                    .Select(x => x.Key).ToList();

                var clients = await _customerProxy.GetPagedAsync(1, clientIds.Count(), clientIds);

                foreach (var order in result.Items)
                {
                    order.Client = clients.Items.Single(x => x.ClientId == order.ClientId);
                }
            }

            return result;
        }

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

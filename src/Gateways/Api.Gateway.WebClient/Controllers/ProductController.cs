using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogProxy _catalogProxy;

        public ProductController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page, int take) => await _catalogProxy.GetPagedAsync(page, take);

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id) => await _catalogProxy.GetAsync(id);

    }
}

using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Gateway.Proxies
{
    public interface ICatalogProxy
    {
        Task<DataCollection<ProductDto>> GetPagedAsync(int page, int take, IEnumerable<int> products = null);
        Task<ProductDto> GetAsync(int id);
    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
        }

        public async Task<DataCollection<ProductDto>> GetPagedAsync(int page, int take, IEnumerable<int> products = null)
        {
            var ids = string.Join(",", products ?? new List<int>());
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}v1/products?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}v1/products/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ProductDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        
    }
}

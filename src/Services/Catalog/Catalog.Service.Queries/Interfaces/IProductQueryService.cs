using Catalog.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Catalog.Service.Queries.Interfaces
{
    public interface IProductQueryService
    {
        Task<DataCollection<ProductDto>> GetPagedAsync(int page, int take, IEnumerable<int> products = null);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetAsync(int id);
    }
}


using Customer.Persistence.Database;
using Customer.Service.Queries.DTOs;
using Customer.Service.Queries.Interfaces;
using Service.Common.Paging;
using Service.Common.Mapping;
using Service.Common.Collection;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Queries.Servives
{
    public class ClientQueryService : IClientQueryService
    {
        private readonly CustomerDbContext _dbContext;

        public ClientQueryService(CustomerDbContext dbContext) { _dbContext = dbContext; }

        public async Task<DataCollection<ClientDto>> GetPagedAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _dbContext.Clients
                .Where(x => clients == null || clients.Contains(x.ClientId))
                .OrderBy(or => or.ClientId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var collection = await _dbContext.Clients.ToListAsync();
            return collection.MapTo<IEnumerable<ClientDto>>();
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == id);
            return entity.MapTo<ClientDto>();
        }

        
    }
}

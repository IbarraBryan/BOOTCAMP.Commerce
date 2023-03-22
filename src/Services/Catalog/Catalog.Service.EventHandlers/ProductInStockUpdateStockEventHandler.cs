using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Catalog.Common.Enums;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateStockCommand>
    {
        private readonly CatalogDbContext _context;
        private readonly ILogger<ProductInStockUpdateStockEventHandler> _logger;

        public ProductInStockUpdateStockEventHandler(CatalogDbContext context, ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ProductInStockUpdateStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- ProductInStockUpdateStockCommand started"); 

            var products = command.Items.Select(x => x.ProductId).ToList();
            var stocks = await _context.ProductInStocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            _logger.LogInformation("--  Retrieve stocks from database..");
            foreach ( var item in command.Items)
            {
                var entry  = stocks.FirstOrDefault(x => x.ProductId == item.ProductId);

                if(item.Action == ProductInStockAction.Substract)
                {
                    if(entry == null || item.Stock > entry.Stock)
                    {
                        _logger.LogError($"--- Poduct {item.ProductId} doens't have enough stock..");
                        throw new ProductInStockUpdateStockCommandException($"--- Poduct {item.ProductId} doens't have enough stock..");
                    }

                    entry.Stock -= item.Stock;

                    _logger.LogInformation($"--- Product {item.ProductId} - its stock was subtracted and its new stock is {entry.Stock}");
                }
                else
                {
                    if(entry == null)
                    {
                        entry = new ProductInStock
                        {
                            ProductId = item.ProductId
                        };

                        _logger.LogInformation($"--- New stock record was created for {entry.ProductId} because didn't exists before..");

                        await _context.AddAsync(entry);
                    }

                    _logger.LogInformation($"--- Add Stock to product {entry.ProductId}..");
                    entry.Stock += item.Stock;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}

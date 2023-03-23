using MediatR;
using Microsoft.Extensions.Logging;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<OrderCreateEventHandler> _logger;
        private readonly ICatalogProxy _catalogProxy;

        public OrderCreateEventHandler(
            OrderDbContext context, 
            ILogger<OrderCreateEventHandler> logger, 
            ICatalogProxy catalogProxy)
        {
            _context = context;
            _logger = logger;
            _catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New order creation started..");
            var entry = new Domain.Order();  
            using(var trx = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                // 01. Prepare detail 
                _logger.LogInformation("--- Prepare detail");
                PrepareDetail(entry, command);

                // 02. Prepare header
                _logger.LogInformation("---  Prepare header");
                PrepareHeader(entry, command);

                // 03. Create Order
                _logger.LogInformation("---  Creating Order");
                await _context.AddAsync(entry);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"---  Order {entry.OrderId} was created.");
                // 04. Update Stock
                _logger.LogInformation("---  Updating Stock");
                await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                {
                    Items = command.Items.Select(x => new ProductInStockUpdateItem { 
                        ProductId = x.ProductId,
                        Stock = x.Quantity,
                        Action = ProductInStockAction.Substract
                    })
                });

                await trx.CommitAsync(cancellationToken);
            }

            _logger.LogInformation("---  New order creation ended..");
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand command)
        {
            entry.Items = command.Items.Select(s => new Domain.OrderDetail { 
                ProductId = s.ProductId,
                Quantity = s.Quantity,
                UnitPrice = s.UnitPrice,
                Total = s.Quantity * s.UnitPrice
            }).ToList();
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand command)
        {
            // header information
            entry.Status = Common.Enums.OrderStatus.Pending;
            entry.PaymentType = command.PaymentType;
            entry.ClientId = command.ClientId;
            entry.CreateAt = DateTime.Now;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }

    }
}

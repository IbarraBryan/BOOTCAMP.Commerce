using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;

namespace Customer.Service.EventHandlers
{
    public class ClientEventHandler : INotificationHandler<ClientCreateCommand>
    {
        private readonly CustomerDbContext _context;

        public ClientEventHandler(CustomerDbContext context) { _context = context; }


        public async Task Handle(ClientCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Client
            {
                Name = command.Name
            });

            await _context.SaveChangesAsync();
        }
    }
}

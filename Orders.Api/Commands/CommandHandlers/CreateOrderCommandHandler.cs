using Ardalis.GuardClauses;
using Orders.Api.Events;
using Orders.Api.Repositories;

namespace Orders.Api.Commands.CommandHandlers
{
    public class CreateOrderCommandHandler(IEventStoreRepository eventStoreRepository)
        : ICommandHandler<CreateOrderCommand>
    {
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task HandleAsync(CreateOrderCommand command)
        {
            Guard.Against.Null(command?.Order);
            await _eventStoreRepository.PublishAsync(new OrderCreatedEvent(command.Order));
        }
    }
}

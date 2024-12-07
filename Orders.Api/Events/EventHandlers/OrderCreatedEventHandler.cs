using Ardalis.GuardClauses;
using Orders.Api.Repositories;

namespace Orders.Api.Events.EventHandlers
{
    public class OrderCreatedEventHandler(IWriteOrdersRepository repository, IReadOrdersRepository readOrdersRepository)
        : IEventHandler<OrderCreatedEvent>
    {
        private readonly IWriteOrdersRepository _repository = repository;
        private readonly IReadOrdersRepository _readOrdersRepository = readOrdersRepository;

        public async Task HandleAsync(OrderCreatedEvent @event)
        {
            Guard.Against.Null(@event);
            var order = await _readOrdersRepository.GetOrderByIdAsync(@event.Order.Id);
            if (order != null)
            {
                await _repository.UpdateOrderAsync(@event.Order);
            }
            else
            {
                await _repository.CreateOrderAsync(@event.Order);
            }

        }
    }
}

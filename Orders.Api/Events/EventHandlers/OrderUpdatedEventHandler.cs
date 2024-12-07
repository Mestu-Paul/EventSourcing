using Ardalis.GuardClauses;
using Orders.Api.Repositories;

namespace Orders.Api.Events.EventHandlers
{
    public class OrderUpdatedEventHandler(IWriteOrdersRepository repository) : IEventHandler<OrderUpdatedEvent>
    {
        private readonly IWriteOrdersRepository _repository = repository;

        public async Task HandleAsync(OrderUpdatedEvent @event)
        {
            Guard.Against.Null(@event);
            await _repository.UpdateOrderAsync(@event.Order);

        }
    }
}

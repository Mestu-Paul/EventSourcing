using EventSourcingApi.Entities;

namespace Orders.Api.Events
{
    public class OrderCreatedEvent(Order order) : IEvent
    {
        public Order Order { get; } = order;
    }
}

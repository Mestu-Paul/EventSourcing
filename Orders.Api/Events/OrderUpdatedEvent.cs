using EventSourcingApi.Entities;

namespace Orders.Api.Events
{
    public class OrderUpdatedEvent(Order order) : IEvent
    {
        public Order Order { get; } = order;
    }
}

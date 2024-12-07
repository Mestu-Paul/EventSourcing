using EventSourcingApi.Entities;

namespace Orders.Api.Commands
{
    public class CreateOrderCommand(Order order) : ICommand
    {
        public Order Order { get; } = order;
    }
}

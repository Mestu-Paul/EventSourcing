using EventSourcingApi.Entities;

namespace Orders.Api.Commands
{
    public class UpdateOrderCommand : ICommand
    {
        public Order Order { get; set; }

        public UpdateOrderCommand(Order order)
        {
            Order = order;
        }
    }
}

namespace Orders.Api.Queries
{
    public class GetOrderByIdQuery : IQuery
    {
        public Guid OrderId { get; set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

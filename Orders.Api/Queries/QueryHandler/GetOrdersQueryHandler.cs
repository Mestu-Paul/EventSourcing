using EventSourcingApi.Entities;

namespace Orders.Api.Queries.QueryHandler
{
    public class GetOrdersQueryHandler : IQueryHandler<GetOrderQuery,IEnumerable<Order>>
    {
        public Task<IEnumerable<Order>> HandleAsync(GetOrderQuery query)
        {
            throw new NotImplementedException();
        }
    }
}

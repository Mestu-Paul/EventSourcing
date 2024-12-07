using Ardalis.GuardClauses;
using EventSourcingApi.Entities;
using Orders.Api.Repositories;

namespace Orders.Api.Queries.QueryHandler
{
    public class GetOrdersByIdQueryHandler : IQueryHandler<GetOrderByIdQuery,Order>
    {
        private readonly IReadOrdersRepository _readOrdersRepository;

        public GetOrdersByIdQueryHandler(IReadOrdersRepository readOrdersRepository)
        {
            _readOrdersRepository=readOrdersRepository;
        }

        public async Task<Order> HandleAsync(GetOrderByIdQuery query)
        {
            Guard.Against.Default(query?.OrderId);
            return await _readOrdersRepository.GetOrderByIdAsync(query.OrderId);
        }
    }
}

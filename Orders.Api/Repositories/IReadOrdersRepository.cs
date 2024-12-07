using EventSourcingApi.Entities;

namespace Orders.Api.Repositories
{
    public interface IReadOrdersRepository
    {
        Task<IEnumerable<Order>> GetReadOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid  orderId);
    }
}

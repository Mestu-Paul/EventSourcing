using EventSourcingApi.Entities;
using Microsoft.EntityFrameworkCore;
using Orders.Api.Data;

namespace Orders.Api.Repositories
{
    public class ReadOrdersRepository : IReadOrdersRepository
    {
        private readonly AppDbContext _context;

        public ReadOrdersRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetReadOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}

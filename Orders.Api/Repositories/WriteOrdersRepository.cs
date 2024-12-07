using EventSourcingApi.Entities;
using Orders.Api.Data;

namespace Orders.Api.Repositories
{
    public class WriteOrdersRepository : IWriteOrdersRepository
    {
        private readonly AppDbContext _context;

        public WriteOrdersRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Order> CreateOrderAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> UpdateOrderAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteOrderAsync(Order entity)
        {
            _context.Orders.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

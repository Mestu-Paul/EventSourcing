using EventSourcingApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orders.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>  options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }

}

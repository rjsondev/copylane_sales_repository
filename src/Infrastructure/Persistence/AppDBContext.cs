using CopylaneSalesInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Infrastructure.Persistence
{
    public sealed class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDBContext).Assembly);
        }
    }
}

using CopylaneSalesInventory.Application.Common.Interfaces;
using CopylaneSalesInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Infrastructure.Persistence;

public class AppDBContext : DbContext, IApplicationDbContext
{
    public AppDBContext(
        DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDBContext).Assembly);
    }
}
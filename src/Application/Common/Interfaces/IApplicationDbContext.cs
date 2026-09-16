using CopylaneSalesInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Product { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
using CopylaneSalesInventory.Application.Common.Exceptions;
using CopylaneSalesInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Application.Products.Commands.SetProductStatus;

public sealed class SetProductStatusHandler : IRequestHandler<SetProductStatusCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public SetProductStatusHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(SetProductStatusCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Product.FirstOrDefaultAsync(
            x => x.Id == request.Id,
                cancellationToken);

        if (product is null)
        {
            throw new NotFoundException($"Product {request.Id} was not found.");
        }

        product.IsActive = request.IsActive;
        product.ModifiedDate = DateTime.UtcNow;
        product.ModifiedById = request.ModifiedById;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

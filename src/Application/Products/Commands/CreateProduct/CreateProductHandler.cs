using CopylaneSalesInventory.Application.Common.Exceptions;
using CopylaneSalesInventory.Application.Common.Interfaces;
using CopylaneSalesInventory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Application.Products.Commands.CreateProduct;

public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Product.AnyAsync(x => x.Sku == request.Sku, cancellationToken);

        if (exists)
        {
            throw new ConflictException($"Product SKU '{request.Sku}' already exists.");
        }

        var product = new Product
        {
            Sku = request.Sku,
            Name = request.Name,
            BarCode = request.Barcode,
            Description = request.Description,
            UnitPrice = request.UnitPrice,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedById = request.CreatedById,
        };

        _dbContext.Product.Add(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}

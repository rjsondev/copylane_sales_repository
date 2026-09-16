using CopylaneSalesInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CopylaneSalesInventory.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductHandler
    : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProductHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _dbContext.Product.FirstOrDefaultAsync(
            x => x.Id == request.Id,
            cancellationToken);

        if (product is null)
        {
            throw new KeyNotFoundException(
                $"Product {request.Id} was not found.");
        }

        var duplicateSku = await _dbContext.Product.AnyAsync(
            x => x.Sku == request.Sku &&
            x.Id != request.Id,
            cancellationToken);

        if (duplicateSku)
        {
            throw new InvalidOperationException(
                $"Product SKU '{request.Sku}' already exists.");
        }

        product.Sku = request.Sku;
        product.BarCode = request.Barcode;
        product.Name = request.Name;
        product.Description = request.Description;
        product.UnitPrice = request.UnitPrice;
        product.ReorderLevel = request.Reorderlevel;
        product.ModifiedDate = DateTime.UtcNow;
        product.ModifiedById = request.ModifiedById;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

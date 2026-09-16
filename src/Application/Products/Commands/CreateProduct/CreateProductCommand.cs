using MediatR;

namespace CopylaneSalesInventory.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Sku,
    string? Barcode,
    string Name,
    string? Description,
    decimal UnitPrice,
    int Reorderlevel,
    int CreatedById
    ) : IRequest<int>;
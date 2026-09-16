using MediatR;

namespace CopylaneSalesInventory.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery
    : IRequest<IReadOnlyList<ProductDto>>;

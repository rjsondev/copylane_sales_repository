using CopylaneSalesInventory.Application.Products.Commands.CreateProduct;
using MediatR;

namespace CopylaneSalesInventory.API.Features.Products;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        group.MapPost("/", CreateProduct);

        return app;
    }

    private static async Task<IResult> CreateProduct(CreateProductCommand command, ISender sender, CancellationToken cancellationToken)
    {
        var productId = await sender.Send(command, cancellationToken);

        return Results.Created($"/api/products/{productId}", new { id = productId });
    }
}

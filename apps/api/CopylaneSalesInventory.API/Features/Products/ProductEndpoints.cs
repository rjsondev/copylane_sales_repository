using CopylaneSalesInventory.Application.Products.Commands.CreateProduct;
using CopylaneSalesInventory.Application.Products.Commands.UpdateProduct;
using MediatR;

namespace CopylaneSalesInventory.API.Features.Products;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/product").WithTags("Products");

        group.MapPost("/", CreateProduct);
        group.MapPut("/{id:int}", UpdateProduct);

        return app;
    }

    private static async Task<IResult> CreateProduct(CreateProductCommand command, ISender sender, CancellationToken cancellationToken)
    {
        var productId = await sender.Send(command, cancellationToken);

        return Results.Created($"/api/product/{productId}", new { id = productId });
    }

    private static async Task<IResult> UpdateProduct(int id, UpdateProductCommand command, ISender sender,CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(
            command,
            cancellationToken);

        return Results.NoContent();
    }
}

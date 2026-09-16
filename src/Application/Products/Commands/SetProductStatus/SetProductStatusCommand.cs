using MediatR;

namespace CopylaneSalesInventory.Application.Products.Commands.SetProductStatus;

public sealed record SetProductStatusCommand(
    int Id,
    bool IsActive,
    int ModifiedById
) : IRequest;

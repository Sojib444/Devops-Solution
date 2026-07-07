using MediatR;

namespace InventoryService.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity) : IRequest<int>;

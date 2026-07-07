using MediatR;
using InventoryService.Application.Products.Common;

namespace InventoryService.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

using MediatR;
using InventoryService.Application.Products.Common;

namespace InventoryService.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;

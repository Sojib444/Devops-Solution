using MediatR;
using InventoryService.Application.Common.Interfaces;
using InventoryService.Application.Products.Common;

namespace InventoryService.Application.Products.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity)).ToList();
    }
}

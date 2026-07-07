using MediatR;
using InventoryService.Application.Common.Interfaces;
using InventoryService.Application.Products.Common;

namespace InventoryService.Application.Products.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null) return null;

        return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity);
    }
}

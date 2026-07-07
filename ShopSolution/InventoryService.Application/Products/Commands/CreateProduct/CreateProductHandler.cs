using MediatR;
using InventoryService.Application.Common.Interfaces;
using InventoryService.Domain.Entities;

namespace InventoryService.Application.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Price, request.StockQuantity);
        var created = await _productRepository.AddAsync(product);
        return created.Id;
    }
}

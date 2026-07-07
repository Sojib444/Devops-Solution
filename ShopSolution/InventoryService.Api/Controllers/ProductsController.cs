using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryService.Application.Products.Commands.CreateProduct;
using InventoryService.Application.Products.Queries.GetAllProducts;
using InventoryService.Application.Products.Queries.GetProductById;

namespace InventoryService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Orders.Queries.GetAllOrders;
using OrderService.Application.Orders.Queries.GetOrderById;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }
}

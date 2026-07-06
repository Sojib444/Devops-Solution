using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.CreateUser;

namespace UserService.Api.Controllers;

[Route("api/[controller]")]
public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var id = await Mediator.Send(command);

        return Ok(id);
    }
}
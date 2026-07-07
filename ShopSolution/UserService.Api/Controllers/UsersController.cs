using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.CreateUser;
using UserService.Application.Users.Commands.DeleteUser;
using UserService.Application.Users.Commands.UpdateUser;
using UserService.Application.Users.Queries.GetAllUsers;
using UserService.Application.Users.Queries.GetUserById;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await Mediator.Send(new GetUserByIdQuery(id));

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await Mediator.Send(new GetAllUsersQuery());

        return Ok(users);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch.");

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}
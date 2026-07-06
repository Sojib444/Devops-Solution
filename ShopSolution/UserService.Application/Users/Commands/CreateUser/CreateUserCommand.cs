using MediatR;

namespace UserService.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email
) : IRequest<int>;
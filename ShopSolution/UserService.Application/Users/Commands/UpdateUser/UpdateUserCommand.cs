using MediatR;

namespace UserService.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(int Id, string FirstName, string LastName, string Email) : IRequest;

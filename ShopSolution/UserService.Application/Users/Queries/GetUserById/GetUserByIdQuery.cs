using MediatR;
using UserService.Application.Users.Common;

namespace UserService.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;

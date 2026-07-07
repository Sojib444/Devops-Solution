using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Application.Users.Common;

namespace UserService.Application.Users.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return null;

        return new UserDto(user.Id, user.FirstName, user.LastName, user.Email);
    }
}

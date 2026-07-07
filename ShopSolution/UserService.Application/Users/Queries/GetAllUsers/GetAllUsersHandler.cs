using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Application.Users.Common;

namespace UserService.Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email)).ToList();
    }
}

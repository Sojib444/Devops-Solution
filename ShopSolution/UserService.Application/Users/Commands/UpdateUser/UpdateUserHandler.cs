using MediatR;
using UserService.Application.Common.Interfaces;

namespace UserService.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            throw new Exception("User not found.");

        var exists = await _userRepository.ExistsByEmailAsync(request.Email, request.Id);

        if (exists)
            throw new Exception("Email already exists.");

        user.Update(request.FirstName, request.LastName, request.Email);

        await _userRepository.UpdateAsync(user);
    }
}

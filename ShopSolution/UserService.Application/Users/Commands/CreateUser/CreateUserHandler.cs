using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Check if email already exists
            var exists = await _userRepository.ExistsByEmailAsync(request.Email);

            if (exists)
                throw new Exception("Email already exists.");

            // Step 2: Create Domain Entity
            var user = new User(
                request.FirstName,
                request.LastName,
                request.Email);

            // Step 3: Save User
            var createdUser = await _userRepository.AddAsync(user);

            // Step 4: Return Id
            return createdUser.Id;
        }
    }
}

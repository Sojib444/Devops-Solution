using FluentValidation;

namespace UserService.Application.Users.Commands.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}

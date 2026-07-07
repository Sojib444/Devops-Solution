using FluentValidation;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerEmail)
            .NotEmpty().EmailAddress();

        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).GreaterThan(0);
                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.UnitPrice).GreaterThan(0);
            });
    }
}

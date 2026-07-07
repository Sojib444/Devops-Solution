using EmailService.Application.Common.Interfaces;
using EmailService.Infrastructure.Messaging.Consumers;
using EmailService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddHostedService<OrderConfirmedConsumer>();

        return services;
    }
}

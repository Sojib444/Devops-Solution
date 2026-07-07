using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Common.Interfaces;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Messaging.Consumers;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Persistence.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        var rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";
        services.AddSingleton<IEventPublisher>(new RabbitMqEventPublisher(rabbitHost));
        services.AddHostedService<StockReservedConsumer>();

        return services;
    }
}

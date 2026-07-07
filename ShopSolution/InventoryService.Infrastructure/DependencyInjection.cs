using InventoryService.Application.Common.Interfaces;
using InventoryService.Infrastructure.Messaging;
using InventoryService.Infrastructure.Messaging.Consumers;
using InventoryService.Infrastructure.Persistence;
using InventoryService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();

        var rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";
        services.AddSingleton<IEventPublisher>(new RabbitMqEventPublisher(rabbitHost));
        services.AddHostedService<OrderCreatedConsumer>();

        return services;
    }
}

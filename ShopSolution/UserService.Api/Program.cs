using Microsoft.EntityFrameworkCore;
using UserService.Application;
using UserService.Infrastructure;
using UserService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

//medirR configuration
builder.Services.AddApplication();

builder.Services.AddControllers();
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "UserService API"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

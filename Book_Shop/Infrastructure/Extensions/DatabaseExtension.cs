using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension;

public static class DatabaseExtension
{
    public static IServiceCollection ConfigureDataServices(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                options.UseMySQL(connectionString)
                );
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}

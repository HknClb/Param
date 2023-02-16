using Application.Abstractions.Repositories.Products;
using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Products;
using Persistence.Services;

namespace Persistence
{
    public static class PersistenceServiceRegistrations
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbRestfulApi"), options => options.MigrationsAssembly("Persistence"));
                //options.UseInMemoryDatabase("DbRestfulApi");
                //options.EnableSensitiveDataLogging(); // Use for view executed sql queries.
            });
            //ProductsContextSeed.SeedAsync(services.BuildServiceProvider().GetRequiredService<BaseDbContext>()).GetAwaiter().GetResult(); // In Memory Database Seed

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
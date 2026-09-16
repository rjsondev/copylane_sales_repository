using CopylaneSalesInventory.Application.Common.Behaviors;
using CopylaneSalesInventory.Application.Common.Interfaces;
using CopylaneSalesInventory.Application.Products.Commands.CreateProduct;
using CopylaneSalesInventory.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopylaneSalesInventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDBContext>());

            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);

                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // NOTE:
            // config (configuration object)
            // an instance of MediatRServiceConfiguration
            // provided by the AddMediatR method.
            // It allows you to configure MediatR services,
            // such as registering handlers, behaviors, and other components.

            return services;
        }
    }
}

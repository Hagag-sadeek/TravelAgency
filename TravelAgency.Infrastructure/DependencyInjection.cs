using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Interfaces;
using TravelAgency.Infrastructure.Repositories;

namespace TravelAgency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Repositories
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            // Add more repositories here as you migrate them

            return services;
        }
    }
}

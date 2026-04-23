using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Services;

namespace TravelAgency.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Application Services
            services.AddScoped<ISupplierService, SupplierService>();

            // Add more services here as you migrate them

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Interfaces;
using TravelAgency.Infrastructure.Repositories;

namespace TravelAgency.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Generic Repository (can be used for simple entities)
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register Specific Repositories with custom queries
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            // Add more specific repositories here as you migrate them

            return services;
        }
    }
}

using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Customer-specific repository
    /// </summary>
    public interface ICustomerRepository : IRepository<Customers>
    {
        /// <summary>
        /// Get all active customers
        /// </summary>
        Task<IEnumerable<Customers>> GetAllActiveAsync();

        /// <summary>
        /// Search customers by phone number
        /// </summary>
        Task<Customers?> GetByPhoneAsync(string phone);

        /// <summary>
        /// Get customer with their tickets
        /// </summary>
        Task<Customers?> GetWithTicketsAsync(int id);

        /// <summary>
        /// Search customers by name or phone
        /// </summary>
        Task<IEnumerable<Customers>> SearchAsync(string searchTerm);
    }
}

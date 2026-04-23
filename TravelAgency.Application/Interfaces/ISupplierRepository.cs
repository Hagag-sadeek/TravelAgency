using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Supplier-specific repository with custom queries
    /// </summary>
    public interface ISupplierRepository : IRepository<Suppliers>
    {
        /// <summary>
        /// Get all active suppliers ordered by SupplierOrder
        /// </summary>
        Task<IEnumerable<Suppliers>> GetAllActiveAsync();

        /// <summary>
        /// Get suppliers ordered by SupplierOrder
        /// </summary>
        Task<IEnumerable<Suppliers>> GetAllOrderedAsync();

        /// <summary>
        /// Get suppliers with their associated tickets
        /// </summary>
        Task<Suppliers?> GetWithTicketsAsync(int id);
    }
}

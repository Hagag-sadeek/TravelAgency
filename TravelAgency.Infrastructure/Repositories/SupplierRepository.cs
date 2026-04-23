using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    /// <summary>
    /// Supplier repository with entity-specific queries
    /// Inherits common CRUD from Repository<Suppliers>
    /// </summary>
    public class SupplierRepository : Repository<Suppliers>, ISupplierRepository
    {
        public SupplierRepository(TravelAgencyContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all active suppliers ordered by SupplierOrder
        /// </summary>
        public async Task<IEnumerable<Suppliers>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(s => s.IsActive)
                .OrderBy(s => s.SupplierOrder)
                .ToListAsync();
        }

        /// <summary>
        /// Get all suppliers ordered by SupplierOrder
        /// </summary>
        public async Task<IEnumerable<Suppliers>> GetAllOrderedAsync()
        {
            return await _dbSet
                .OrderBy(s => s.SupplierOrder)
                .ToListAsync();
        }

        /// <summary>
        /// Get supplier with associated tickets
        /// </summary>
        public async Task<Suppliers?> GetWithTicketsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.SupplierId == id);
        }

        // Note: Common CRUD operations (GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync, etc.)
        // are inherited from Repository<Suppliers> base class
    }
}

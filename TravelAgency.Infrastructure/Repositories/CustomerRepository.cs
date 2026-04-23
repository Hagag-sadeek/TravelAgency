using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    /// <summary>
    /// Customer repository with entity-specific queries
    /// </summary>
    public class CustomerRepository : Repository<Customers>, ICustomerRepository
    {
        public CustomerRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customers>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }

        public async Task<Customers?> GetByPhoneAsync(string phone)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Phone1 == phone || c.Phone2 == phone || c.Phone3 == phone);
        }

        public async Task<Customers?> GetWithTicketsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<IEnumerable<Customers>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(c => c.IsActive && 
                    (c.FullName != null && c.FullName.Contains(searchTerm) ||
                     c.Phone1 != null && c.Phone1.Contains(searchTerm) ||
                     c.Code != null && c.Code.Contains(searchTerm)))
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }
    }
}

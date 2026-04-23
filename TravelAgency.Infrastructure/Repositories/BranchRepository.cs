using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    /// <summary>
    /// Branch repository with entity-specific queries
    /// </summary>
    public class BranchRepository : Repository<Branches>, IBranchRepository
    {
        public BranchRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Branches>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(b => b.IsActive)
                .OrderBy(b => b.BranchOrder)
                .ToListAsync();
        }

        public async Task<Branches?> GetWithUsersAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Users)
                .FirstOrDefaultAsync(b => b.BranchId == id);
        }
    }
}

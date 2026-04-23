using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Branch-specific repository
    /// </summary>
    public interface IBranchRepository : IRepository<Branches>
    {
        /// <summary>
        /// Get all active branches ordered by BranchOrder
        /// </summary>
        Task<IEnumerable<Branches>> GetAllActiveAsync();

        /// <summary>
        /// Get branch with associated users
        /// </summary>
        Task<Branches?> GetWithUsersAsync(int id);
    }
}

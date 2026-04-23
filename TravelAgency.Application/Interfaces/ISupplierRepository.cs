using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Suppliers>> GetAllActiveAsync();
        Task<Suppliers?> GetByIdAsync(int id);
        Task<Suppliers> AddAsync(Suppliers supplier);
        Task<Suppliers> UpdateAsync(Suppliers supplier);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly TravelAgencyContext _context;

        public SupplierRepository(TravelAgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Suppliers>> GetAllActiveAsync()
        {
            return await _context.Suppliers
                .Where(s => s.IsActive)
                .OrderBy(s => s.SupplierOrder)
                .ToListAsync();
        }

        public async Task<Suppliers?> GetByIdAsync(int id)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);
        }

        public async Task<Suppliers> AddAsync(Suppliers supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            return supplier;
        }

        public async Task<Suppliers> UpdateAsync(Suppliers supplier)
        {
            _context.Suppliers.Update(supplier);
            return await Task.FromResult(supplier);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await GetByIdAsync(id);
            if (supplier == null)
            {
                return false;
            }

            supplier.IsActive = false;
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Suppliers.AnyAsync(s => s.SupplierId == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

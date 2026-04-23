using TravelAgency.Application.DTOs.Suppliers;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;

namespace TravelAgency.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllActiveSuppliersAsync()
        {
            var suppliers = await _repository.GetAllActiveAsync();
            return suppliers.Select(s => s.ToDto());
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            return supplier?.ToDto();
        }

        public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createDto)
        {
            var supplier = createDto.ToEntity();
            var createdSupplier = await _repository.AddAsync(supplier);
            await _repository.SaveChangesAsync();
            return createdSupplier.ToDto();
        }

        public async Task<SupplierDto?> UpdateSupplierAsync(UpdateSupplierDto updateDto)
        {
            var existingSupplier = await _repository.GetByIdAsync(updateDto.SupplierId);
            if (existingSupplier == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingSupplier);
           
            await _repository.UpdateAsync(existingSupplier);
            await _repository.SaveChangesAsync();

            return existingSupplier.ToDto();
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<bool> SupplierExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}

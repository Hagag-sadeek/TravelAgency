using TravelAgency.Application.DTOs.Suppliers;

namespace TravelAgency.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllActiveSuppliersAsync();
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createDto);
        Task<SupplierDto?> UpdateSupplierAsync(UpdateSupplierDto updateDto);
        Task<bool> DeleteSupplierAsync(int id);
        Task<bool> SupplierExistsAsync(int id);
    }
}

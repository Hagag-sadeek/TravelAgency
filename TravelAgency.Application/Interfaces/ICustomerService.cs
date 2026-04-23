using TravelAgency.Application.DTOs.Customers;

namespace TravelAgency.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllActiveCustomersAsync();
        Task<IEnumerable<CustomerDto>> GetCustomersNeedingUpdateAsync();
        Task<IEnumerable<CustomerDto>> GetUpdatedCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto);
        Task<CustomerDto?> UpdateCustomerAsync(UpdateCustomerDto updateDto);
        Task<bool> DeleteCustomerAsync(int id);
        Task<CustomerDto> QuickAddCustomerAsync(QuickAddCustomerDto dto);
        Task<CustomerDto?> GetCustomerByPhoneAsync(string phone);
        Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm);
    }
}

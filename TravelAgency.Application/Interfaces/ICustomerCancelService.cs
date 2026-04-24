using TravelAgency.Application.DTOs.CustomerCancels;

namespace TravelAgency.Application.Interfaces
{
    public interface ICustomerCancelService
    {
        Task<CustomerCancelDto> CreateCustomerCancelAsync(CreateCustomerCancelDto createDto);
        Task<IEnumerable<CustomerCancelDto>> GetByCustomerIdAsync(int customerId);
    }
}

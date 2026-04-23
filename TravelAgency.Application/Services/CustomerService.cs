using TravelAgency.Application.DTOs.Customers;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllActiveCustomersAsync()
        {
            var customers = await _repository.GetAllActiveAsync();
            return customers.Select(c => c.ToDto());
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersNeedingUpdateAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers
                .Where(c => c.IsActive && c.NeedUpdate)
                .Select(c => c.ToDto());
        }

        public async Task<IEnumerable<CustomerDto>> GetUpdatedCustomersAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers
                .Where(c => c.IsActive && c.Updated)
                .Select(c => c.ToDto());
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return customer?.ToDto();
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto)
        {
            var customer = createDto.ToEntity();
            var createdCustomer = await _repository.AddAsync(customer);
            await _repository.SaveChangesAsync();
            return createdCustomer.ToDto();
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(UpdateCustomerDto updateDto)
        {
            var existingCustomer = await _repository.GetByIdAsync(updateDto.CustomerId);
            if (existingCustomer == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingCustomer);
            await _repository.UpdateAsync(existingCustomer);
            await _repository.SaveChangesAsync();

            return existingCustomer.ToDto();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<CustomerDto> QuickAddCustomerAsync(QuickAddCustomerDto dto)
        {
            // Check if customer exists by phone
            var existingCustomer = await _repository.GetByPhoneAsync(dto.Phone.Trim());

            if (existingCustomer != null)
            {
                // Update existing customer's name
                existingCustomer.FullName = dto.Name.Trim();
                await _repository.UpdateAsync(existingCustomer);
                await _repository.SaveChangesAsync();
                return existingCustomer.ToDto();
            }
            else
            {
                // Generate new code
                var allCustomers = await _repository.GetAllAsync();
                var lastCustomer = allCustomers.OrderByDescending(c => c.CustomerId).FirstOrDefault();
                int newCode = (lastCustomer != null && int.TryParse(lastCustomer.Code, out int lastCode)) 
                    ? lastCode + 1 
                    : 1;

                // Create new customer
                var newCustomer = new Customers
                {
                    FullName = dto.Name.Trim(),
                    Phone1 = dto.Phone.Trim(),
                    Code = newCode.ToString(),
                    Points = 0,
                    IsActive = true,
                    SendWhatsApp = true,
                    NeedUpdate = false,
                    Updated = false
                };

                await _repository.AddAsync(newCustomer);
                await _repository.SaveChangesAsync();
                return newCustomer.ToDto();
            }
        }

        public async Task<CustomerDto?> GetCustomerByPhoneAsync(string phone)
        {
            var customer = await _repository.GetByPhoneAsync(phone);
            return customer?.ToDto();
        }

        public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string searchTerm)
        {
            var customers = await _repository.SearchAsync(searchTerm);
            return customers.Select(c => c.ToDto());
        }
    }
}

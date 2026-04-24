using TravelAgency.Application.DTOs.CustomerCancels;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Services
{
    public class CustomerCancelService : ICustomerCancelService
    {
        private readonly IRepository<CustomerCancels> _repository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerCancelService(
            IRepository<CustomerCancels> repository,
            ICustomerRepository customerRepository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerCancelDto> CreateCustomerCancelAsync(CreateCustomerCancelDto createDto)
        {
            var customerCancel = new CustomerCancels
            {
                CustomerId = createDto.CustomerId,
                comment = createDto.Comment,
                CancelDate = DateTime.Now.Date
            };

            await _repository.AddAsync(customerCancel);
            await _repository.SaveChangesAsync();

            var customer = await _customerRepository.GetByIdAsync(createDto.CustomerId);

            return new CustomerCancelDto
            {
                Id = customerCancel.Id,
                CustomerId = customerCancel.CustomerId,
                CustomerName = customer?.FullName,
                Comment = customerCancel.comment,
                CancelDate = customerCancel.CancelDate
            };
        }

        public async Task<IEnumerable<CustomerCancelDto>> GetByCustomerIdAsync(int customerId)
        {
            var cancels = await _repository.GetAllAsync();
            var customer = await _customerRepository.GetByIdAsync(customerId);

            return cancels
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerCancelDto
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    CustomerName = customer?.FullName,
                    Comment = c.comment,
                    CancelDate = c.CancelDate
                });
        }
    }
}

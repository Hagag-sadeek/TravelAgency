using TravelAgency.Application.DTOs.Customers;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class CustomerMappingExtensions
    {
        public static CustomerDto ToDto(this Customers customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Code = customer.Code,
                Job = customer.Job,
                FullName = customer.FullName,
                Adreess1 = customer.Adreess1,
                Adreess2 = customer.Adreess2,
                Phone1 = customer.Phone1,
                Phone2 = customer.Phone2,
                Phone3 = customer.Phone3,
                IsActive = customer.IsActive,
                SendWhatsApp = customer.SendWhatsApp,
                NeedUpdate = customer.NeedUpdate,
                Updated = customer.Updated,
                Points = customer.Points
            };
        }

        public static Customers ToEntity(this CreateCustomerDto dto)
        {
            return new Customers
            {
                Code = dto.Code,
                Job = dto.Job,
                FullName = dto.FullName,
                Adreess1 = dto.Adreess1,
                Adreess2 = dto.Adreess2,
                Phone1 = dto.Phone1,
                Phone2 = dto.Phone2,
                Phone3 = dto.Phone3,
                IsActive = true,
                SendWhatsApp = true,
                NeedUpdate = false,
                Updated = false,
                Points = 0
            };
        }

        public static void UpdateEntity(this UpdateCustomerDto dto, Customers customer)
        {
            customer.Code = dto.Code;
            customer.Job = dto.Job;
            customer.FullName = dto.FullName;
            customer.Adreess1 = dto.Adreess1;
            customer.Adreess2 = dto.Adreess2;
            customer.Phone1 = dto.Phone1;
            customer.Phone2 = dto.Phone2;
            customer.Phone3 = dto.Phone3;
            customer.IsActive = dto.IsActive;
            customer.SendWhatsApp = dto.SendWhatsApp;
            customer.NeedUpdate = dto.NeedUpdate;
            customer.Updated = dto.Updated;
            customer.Points = dto.Points;
        }
    }
}

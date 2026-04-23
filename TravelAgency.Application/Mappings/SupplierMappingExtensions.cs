using TravelAgency.Application.DTOs.Suppliers;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class SupplierMappingExtensions
    {
        public static SupplierDto ToDto(this Suppliers supplier)
        {
            return new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                FullName = supplier.FullName,
                Commision = supplier.Commision,
                SupplierOrder = supplier.SupplierOrder,
                Adreess1 = supplier.Adreess1,
                Phone1 = supplier.Phone1,
                IsActive = supplier.IsActive
            };
        }

        public static Suppliers ToEntity(this CreateSupplierDto dto)
        {
            return new Suppliers
            {
                FullName = dto.FullName,
                Commision = dto.Commision,
                SupplierOrder = dto.SupplierOrder,
                Adreess1 = dto.Adreess1,
                Phone1 = dto.Phone1,
                IsActive = true
            };
        }

        public static void UpdateEntity(this UpdateSupplierDto dto, Suppliers supplier)
        {
            supplier.FullName = dto.FullName;
            supplier.Commision = dto.Commision;
            supplier.SupplierOrder = dto.SupplierOrder;
            supplier.Adreess1 = dto.Adreess1;
            supplier.Phone1 = dto.Phone1;
            supplier.IsActive = dto.IsActive;
        }
    }
}

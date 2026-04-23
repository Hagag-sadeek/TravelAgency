using TravelAgency.Application.DTOs.Branches;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class BranchMappingExtensions
    {
        public static BranchDto ToDto(this Branches branch)
        {
            return new BranchDto
            {
                BranchId = branch.BranchId,
                BranchOrder = branch.BranchOrder,
                Title = branch.Title,
                Description = branch.Description,
                IsActive = branch.IsActive,
                Address = branch.Address,
                Phone = branch.Phone
            };
        }

        public static Branches ToEntity(this CreateBranchDto dto)
        {
            return new Branches
            {
                Title = dto.Title,
                Description = dto.Description,
                BranchOrder = dto.BranchOrder,
                Address = dto.Address,
                Phone = dto.Phone,
                IsActive = true
            };
        }

        public static void UpdateEntity(this UpdateBranchDto dto, Branches branch)
        {
            branch.Title = dto.Title;
            branch.Description = dto.Description;
            branch.BranchOrder = dto.BranchOrder;
            branch.Address = dto.Address;
            branch.Phone = dto.Phone;
            branch.IsActive = dto.IsActive;
        }
    }
}

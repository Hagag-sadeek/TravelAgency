using TravelAgency.Application.DTOs.Users;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static UserDto ToDto(this Users user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Firstname = user.Firstname,
                BranchId = user.BranchId,
                BranchTitle = user.Branch?.Title,
                IsAdmin = user.IsAdmin,
                SupplierId = user.SupplierId,
                SupplierName = user.Supplier?.FullName
            };
        }

        public static Users ToEntity(this CreateUserDto dto)
        {
            return new Users
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Firstname = dto.Firstname,
                BranchId = dto.BranchId,
                IsAdmin = dto.IsAdmin,
                SupplierId = dto.SupplierId
            };
        }

        public static void UpdateEntity(this UpdateUserDto dto, Users user)
        {
            user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = dto.Password;
            }
            user.Firstname = dto.Firstname;
            user.BranchId = dto.BranchId;
            user.IsAdmin = dto.IsAdmin;
            user.SupplierId = dto.SupplierId;
        }
    }
}

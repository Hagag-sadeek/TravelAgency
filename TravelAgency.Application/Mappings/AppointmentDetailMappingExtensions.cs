using TravelAgency.Application.DTOs.AppointmentDetails;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class AppointmentDetailMappingExtensions
    {
        public static AppointmentDetailDto ToDto(this AppointmentDetails detail)
        {
            return new AppointmentDetailDto
            {
                AppointmentDetailId = detail.AppointmentDetailId,
                AppointmentId = detail.AppointmentId,
                AppointmentTitle = detail.Appointment?.Title,
                BranchId = detail.BranchId,
                BranchTitle = detail.Branch?.Title,
                LeaveTime = detail.LeaveTime,
                Price = detail.Price
            };
        }

        public static AppointmentDetails ToEntity(this CreateAppointmentDetailDto dto)
        {
            return new AppointmentDetails
            {
                AppointmentId = dto.AppointmentId,
                BranchId = dto.BranchId,
                LeaveTime = dto.LeaveTime,
                Price = dto.Price
            };
        }

        public static void UpdateEntity(this UpdateAppointmentDetailDto dto, AppointmentDetails detail)
        {
            detail.AppointmentId = dto.AppointmentId;
            detail.BranchId = dto.BranchId;
            detail.LeaveTime = dto.LeaveTime;
            detail.Price = dto.Price;
        }
    }
}

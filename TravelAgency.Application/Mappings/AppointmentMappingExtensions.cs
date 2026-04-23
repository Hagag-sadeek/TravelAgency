using TravelAgency.Application.DTOs.Appointments;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class AppointmentMappingExtensions
    {
        public static AppointmentDto ToDto(this Appointments appointment)
        {
            return new AppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                Title = appointment.Title,
                Description = appointment.Description,
                IsActive = appointment.IsActive,
                SortOrder = appointment.SortOrder
            };
        }

        public static Appointments ToEntity(this CreateAppointmentDto dto)
        {
            return new Appointments
            {
                Title = dto.Title,
                Description = dto.Description,
                SortOrder = dto.SortOrder,
                IsActive = true
            };
        }

        public static void UpdateEntity(this UpdateAppointmentDto dto, Appointments appointment)
        {
            appointment.Title = dto.Title;
            appointment.Description = dto.Description;
            appointment.SortOrder = dto.SortOrder;
            appointment.IsActive = dto.IsActive;
        }
    }
}

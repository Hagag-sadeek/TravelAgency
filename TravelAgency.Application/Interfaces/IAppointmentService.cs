using TravelAgency.Application.DTOs.Appointments;

namespace TravelAgency.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllActiveAppointmentsAsync();
        Task<IEnumerable<AppointmentDto>> GetActiveForUserAsync(IEnumerable<int> appointmentIds);
        Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto);
        Task<AppointmentDto?> UpdateAppointmentAsync(UpdateAppointmentDto updateDto);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}

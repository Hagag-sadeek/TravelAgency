using TravelAgency.Application.DTOs.AppointmentDetails;

namespace TravelAgency.Application.Interfaces
{
    public interface IAppointmentDetailService
    {
        Task<IEnumerable<AppointmentDetailDto>> GetAllWithRelationsAsync();
        Task<AppointmentDetailDto?> GetByIdAsync(int id);
        Task<AppointmentDetailDto?> GetByIdWithRelationsAsync(int id);
        Task<IEnumerable<AppointmentDetailDto>> GetByAppointmentIdAsync(int appointmentId);
        Task<AppointmentDetailDto> CreateAppointmentDetailAsync(CreateAppointmentDetailDto createDto);
        Task<AppointmentDetailDto?> UpdateAppointmentDetailAsync(UpdateAppointmentDetailDto updateDto);
        Task<bool> DeleteAppointmentDetailAsync(int id);
        Task<IEnumerable<(int Id, string Title)>> GetActiveAppointmentsAsync();
        Task<IEnumerable<(int Id, string Title)>> GetActiveBranchesAsync();
    }
}

using TravelAgency.Application.DTOs.Appointments;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;

namespace TravelAgency.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllActiveAppointmentsAsync()
        {
            var appointments = await _repository.GetAllActiveAsync();
            return appointments.Select(a => a.ToDto());
        }

        public async Task<IEnumerable<AppointmentDto>> GetActiveForUserAsync(IEnumerable<int> appointmentIds)
        {
            var appointments = await _repository.GetActiveForUserAsync(appointmentIds);
            return appointments.Select(a => a.ToDto());
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            return appointment?.ToDto();
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto)
        {
            var appointment = createDto.ToEntity();
            var createdAppointment = await _repository.AddAsync(appointment);
            await _repository.SaveChangesAsync();
            return createdAppointment.ToDto();
        }

        public async Task<AppointmentDto?> UpdateAppointmentAsync(UpdateAppointmentDto updateDto)
        {
            var existingAppointment = await _repository.GetByIdAsync(updateDto.AppointmentId);
            if (existingAppointment == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingAppointment);
            await _repository.UpdateAsync(existingAppointment);
            await _repository.SaveChangesAsync();

            return existingAppointment.ToDto();
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }
    }
}

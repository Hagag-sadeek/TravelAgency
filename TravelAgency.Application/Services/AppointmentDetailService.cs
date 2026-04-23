using TravelAgency.Application.DTOs.AppointmentDetails;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;

namespace TravelAgency.Application.Services
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly IAppointmentDetailRepository _repository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBranchRepository _branchRepository;

        public AppointmentDetailService(
            IAppointmentDetailRepository repository,
            IAppointmentRepository appointmentRepository,
            IBranchRepository branchRepository)
        {
            _repository = repository;
            _appointmentRepository = appointmentRepository;
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<AppointmentDetailDto>> GetAllWithRelationsAsync()
        {
            var details = await _repository.GetAllWithRelationsAsync();
            return details.Select(d => d.ToDto());
        }

        public async Task<AppointmentDetailDto?> GetByIdAsync(int id)
        {
            var detail = await _repository.GetByIdAsync(id);
            return detail?.ToDto();
        }

        public async Task<AppointmentDetailDto?> GetByIdWithRelationsAsync(int id)
        {
            var detail = await _repository.GetByIdWithRelationsAsync(id);
            return detail?.ToDto();
        }

        public async Task<IEnumerable<AppointmentDetailDto>> GetByAppointmentIdAsync(int appointmentId)
        {
            var details = await _repository.GetByAppointmentIdAsync(appointmentId);
            return details.Select(d => d.ToDto());
        }

        public async Task<AppointmentDetailDto> CreateAppointmentDetailAsync(CreateAppointmentDetailDto createDto)
        {
            var detail = createDto.ToEntity();
            var createdDetail = await _repository.AddAsync(detail);
            await _repository.SaveChangesAsync();

            // Reload with relations for proper DTO mapping
            var detailWithRelations = await _repository.GetByIdWithRelationsAsync(createdDetail.AppointmentDetailId);
            return detailWithRelations!.ToDto();
        }

        public async Task<AppointmentDetailDto?> UpdateAppointmentDetailAsync(UpdateAppointmentDetailDto updateDto)
        {
            var existingDetail = await _repository.GetByIdAsync(updateDto.AppointmentDetailId);
            if (existingDetail == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingDetail);
            await _repository.UpdateAsync(existingDetail);
            await _repository.SaveChangesAsync();

            // Reload with relations
            var detailWithRelations = await _repository.GetByIdWithRelationsAsync(existingDetail.AppointmentDetailId);
            return detailWithRelations?.ToDto();
        }

        public async Task<bool> DeleteAppointmentDetailAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<(int Id, string Title)>> GetActiveAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllActiveAsync();
            return appointments
                .Where(a => a.Title != null)
                .Select(a => (a.AppointmentId, a.Title!));
        }

        public async Task<IEnumerable<(int Id, string Title)>> GetActiveBranchesAsync()
        {
            var branches = await _branchRepository.GetAllActiveAsync();
            return branches
                .Where(b => b.Title != null)
                .Select(b => (b.BranchId, b.Title!));
        }
    }
}

using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// AppointmentDetail-specific repository
    /// </summary>
    public interface IAppointmentDetailRepository : IRepository<AppointmentDetails>
    {
        /// <summary>
        /// Get all appointment details with related appointment and branch
        /// </summary>
        Task<IEnumerable<AppointmentDetails>> GetAllWithRelationsAsync();

        /// <summary>
        /// Get appointment detail with relations by ID
        /// </summary>
        Task<AppointmentDetails?> GetByIdWithRelationsAsync(int id);

        /// <summary>
        /// Get appointment details for a specific appointment
        /// </summary>
        Task<IEnumerable<AppointmentDetails>> GetByAppointmentIdAsync(int appointmentId);
    }
}

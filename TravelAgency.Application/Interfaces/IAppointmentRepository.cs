using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Appointment-specific repository
    /// </summary>
    public interface IAppointmentRepository : IRepository<Appointments>
    {
        /// <summary>
        /// Get all active appointments ordered by SortOrder
        /// </summary>
        Task<IEnumerable<Appointments>> GetAllActiveAsync();

        /// <summary>
        /// Get all active appointments for a specific user
        /// </summary>
        Task<IEnumerable<Appointments>> GetActiveForUserAsync(IEnumerable<int> appointmentIds);

        /// <summary>
        /// Get appointment with related data (details, tickets, prices)
        /// </summary>
        Task<Appointments?> GetWithDetailsAsync(int id);
    }
}

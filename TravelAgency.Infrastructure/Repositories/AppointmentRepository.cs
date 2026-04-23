using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    /// <summary>
    /// Appointment repository with entity-specific queries
    /// </summary>
    public class AppointmentRepository : Repository<Appointments>, IAppointmentRepository
    {
        public AppointmentRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointments>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(a => a.IsActive)
                .OrderBy(a => a.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointments>> GetActiveForUserAsync(IEnumerable<int> appointmentIds)
        {
            return await _dbSet
                .Where(a => a.IsActive && appointmentIds.Contains(a.AppointmentId))
                .OrderBy(a => a.SortOrder)
                .ToListAsync();
        }

        public async Task<Appointments?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(a => a.AppointmentDetails)
                .Include(a => a.Tickets)
                .Include(a => a.AppointmentPrice)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }
    }
}

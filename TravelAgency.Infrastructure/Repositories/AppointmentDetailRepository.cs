using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    /// <summary>
    /// AppointmentDetail repository with entity-specific queries
    /// </summary>
    public class AppointmentDetailRepository : Repository<AppointmentDetails>, IAppointmentDetailRepository
    {
        public AppointmentDetailRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppointmentDetails>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(ad => ad.Appointment)
                .Include(ad => ad.Branch)
                .ToListAsync();
        }

        public async Task<AppointmentDetails?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(ad => ad.Appointment)
                .Include(ad => ad.Branch)
                .FirstOrDefaultAsync(ad => ad.AppointmentDetailId == id);
        }

        public async Task<IEnumerable<AppointmentDetails>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _dbSet
                .Include(ad => ad.Branch)
                .Where(ad => ad.AppointmentId == appointmentId)
                .ToListAsync();
        }
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Tickets>, ITicketRepository
    {
        public TicketRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tickets>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(t => t.Appointment)
                .Include(t => t.Customer)
                .Include(t => t.Supplier)
                .Include(t => t.ToBranch)
                .Include(t => t.FromBranch)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tickets>> GetByAppointmentAndDateAsync(int appointmentId, DateTime date, bool activeOnly = true)
        {
            var query = _dbSet
                .Include(t => t.Customer)
                .Include(t => t.Supplier)
                .Include(t => t.FromBranch)
                .Include(t => t.ToBranch)
                .Where(t => t.AppointmentId == appointmentId && t.TicketDate.Date == date.Date);

            if (activeOnly)
            {
                query = query.Where(t => t.IsActive);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> IsSeatReservedAsync(int seatNumber, DateTime date, int appointmentId)
        {
            return await _dbSet.AnyAsync(t =>
                t.SeatId == seatNumber &&
                t.TicketDate.Date == date.Date &&
                t.AppointmentId == appointmentId &&
                t.IsActive);
        }

        public async Task<bool> CustomerHasTicketAsync(int customerId, DateTime date, int appointmentId)
        {
            return await _dbSet.AnyAsync(t =>
                t.CustomerId == customerId &&
                t.TicketDate.Date == date.Date &&
                t.AppointmentId == appointmentId &&
                t.IsActive);
        }

        public async Task<Tickets?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Appointment)
                .Include(t => t.Customer)
                .Include(t => t.Supplier)
                .Include(t => t.FromBranch)
                .Include(t => t.ToBranch)
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public async Task<IEnumerable<Tickets>> GetByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(t => t.Appointment)
                .Include(t => t.Supplier)
                .Where(t => t.CustomerId == customerId && t.IsActive)
                .ToListAsync();
        }
    }
}

using System;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Ticket-specific repository with complex queries
    /// </summary>
    public interface ITicketRepository : IRepository<Tickets>
    {
        /// <summary>
        /// Get all tickets with related data (customer, supplier, appointment, branches)
        /// </summary>
        Task<IEnumerable<Tickets>> GetAllWithRelationsAsync();

        /// <summary>
        /// Get tickets for a specific appointment and date with relations
        /// </summary>
        Task<IEnumerable<Tickets>> GetByAppointmentAndDateAsync(int appointmentId, DateTime date, bool activeOnly = true);

        /// <summary>
        /// Check if seat is already reserved
        /// </summary>
        Task<bool> IsSeatReservedAsync(int seatNumber, DateTime date, int appointmentId);

        /// <summary>
        /// Check if customer already has a ticket for this appointment and date
        /// </summary>
        Task<bool> CustomerHasTicketAsync(int customerId, DateTime date, int appointmentId);

        /// <summary>
        /// Get ticket by ID with all relations
        /// </summary>
        Task<Tickets?> GetByIdWithRelationsAsync(int id);

        /// <summary>
        /// Get tickets by customer ID
        /// </summary>
        Task<IEnumerable<Tickets>> GetByCustomerIdAsync(int customerId);
    }
}

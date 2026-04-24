using System;
using TravelAgency.Application.DTOs.Tickets;

namespace TravelAgency.Application.Interfaces
{
    public interface ITicketService
    {
        // Basic CRUD
        Task<IEnumerable<TicketDto>> GetAllTicketsWithRelationsAsync();
        Task<TicketDto?> GetTicketByIdAsync(int id);
        Task<bool> DeleteTicketAsync(int ticketId, int userId);

        // Reservation
        Task<TicketDto> CreateTicketAsync(CreateTicketDto createDto, int userId);

        // Seat management
        Task<bool> IsSeatAvailableAsync(int seatNumber, DateTime date, int appointmentId);
        Task<bool> IsFirstTicketForCustomerAsync(int customerId, DateTime date, int appointmentId);
        Task<IEnumerable<ReservedSeatDto>> GetReservedSeatsAsync(int appointmentId, DateTime date, int userId, bool isAdmin);

        // Ticket actions
        Task<bool> ConfirmTicketPriceAsync(int ticketId, int price);
        Task<bool> ConformTicketAsync(int ticketId);
        Task<bool> CancelConformTicketAsync(int ticketId);
        Task<bool> PayTicketAsync(int ticketId);
        Task<bool> CancelPayTicketAsync(int ticketId);

        // View selection
        Task<string> DetermineViewNameAsync(int appointmentId, DateTime date, bool isAdmin);

        // Customer info
        Task<object?> GetCustomerInfoByTicketIdAsync(int ticketId);

        // Dropdown data
        Task<IEnumerable<(int Id, string Title)>> GetUserAppointmentsAsync(int userId);
    }
}


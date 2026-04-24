using System;
using TravelAgency.Application.DTOs.Reports;

namespace TravelAgency.Application.Interfaces
{
    public interface IReportService
    {
        Task<TicketReportResultDto> GenerateTicketReportAsync(int appointmentId, DateTime ticketDate);
        Task<IEnumerable<(int Id, string Title)>> GetActiveAppointmentsForReportAsync();
    }
}

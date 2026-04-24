using System;

namespace TravelAgency.Application.DTOs.Reports
{
    public class TicketReportDto
    {
        public DateTime TicketDate { get; set; }
        public int AppointmentId { get; set; }
        public string? AppointmentTitle { get; set; }
    }
}

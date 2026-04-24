using System;
using System.Collections.Generic;

namespace TravelAgency.Application.DTOs.Reports
{
    public class TicketReportResultDto
    {
        public DateTime TicketDate { get; set; }
        public int AppointmentId { get; set; }
        public List<SupplierReportDetailDto> SupplierDetails { get; set; } = new();
    }
}

namespace TravelAgency.Application.DTOs.Reports
{
    public class SupplierReportDetailDto
    {
        public string? SupplierName { get; set; }
        public int TicketCount { get; set; }
        public int TotalIncome { get; set; }
        public int NetIncome { get; set; }
    }
}

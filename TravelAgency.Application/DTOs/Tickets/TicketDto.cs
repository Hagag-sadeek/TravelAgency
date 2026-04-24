using System;

namespace TravelAgency.Application.DTOs.Tickets
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public string? AppointmentTitle { get; set; }
        public DateTime TicketDate { get; set; }
        public DateTime? ReserveDate { get; set; }
        public int Price { get; set; }
        public int? FromBranchId { get; set; }
        public string? FromBranchTitle { get; set; }
        public int? ToBranchId { get; set; }
        public string? ToBranchTitle { get; set; }
        public int SeatId { get; set; }
        public string? Comment { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFemale { get; set; }
        public bool IsConformed { get; set; }
    }
}

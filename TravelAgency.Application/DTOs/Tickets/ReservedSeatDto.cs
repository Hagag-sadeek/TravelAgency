using System;

namespace TravelAgency.Application.DTOs.Tickets
{
    public class ReservedSeatDto
    {
        public int TicketId { get; set; }
        public string? CustomerName { get; set; }
        public string? SupplierName { get; set; }
        public string? FromBranch { get; set; }
        public string? ToBranch { get; set; }
        public string? Phone { get; set; }
        public bool IsFemale { get; set; }
        public bool IsConformed { get; set; }
        public bool IsMine { get; set; }
        public int SeatId { get; set; }
        public int Price { get; set; }
        public string? Code { get; set; }
    }
}

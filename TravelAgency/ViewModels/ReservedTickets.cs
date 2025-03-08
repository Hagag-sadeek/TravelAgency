namespace TravelAgency.ViewModels
{
    public class ReservedTickets
    {
        public int TicketId { get; set; }
        public string Customer { get; set; }
        public string Supplier { get; set; }
        public string FromBranch { get; set; }
        public string ToBranch { get; set; }
        public string Phone { get; set; } 
        public bool IsPaid { get; set; }
        public bool IsMine { get; set; }
        public int SeatId { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }

    }
}
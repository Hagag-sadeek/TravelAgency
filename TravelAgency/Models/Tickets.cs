using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class Tickets
    {
        public int TicketId { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime TicketDate { get; set; }
        public DateTime? ReserveDate { get; set; }
        public int Price { get; set; }
        public int? FromBranchId { get; set; }
        public int? ToBranchId { get; set; }
        public int SeatId { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Appointments Appointment { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Branches FromBranch { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual Branches ToBranch { get; set; }
    }
}

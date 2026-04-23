using System;
using System.Collections.Generic;

namespace TravelAgency.Core.Entities
{
    public partial class TicketDistributions
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class Branches
    {
        public Branches()
        {
            AppointmentDetails = new HashSet<AppointmentDetails>();
            TicketsFromBranch = new HashSet<Tickets>();
            TicketsToBranch = new HashSet<Tickets>();
            Users = new HashSet<Users>();
        }

        public int BranchId { get; set; }
        public int BranchOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual ICollection<Tickets> TicketsFromBranch { get; set; }
        public virtual ICollection<Tickets> TicketsToBranch { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}

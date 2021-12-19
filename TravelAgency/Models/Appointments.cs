using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class Appointments
    {
        public Appointments()
        {
            AppointmentPrice = new HashSet<AppointmentPrice>();
            ;
            AppointmentDetails = new HashSet<AppointmentDetails>();
            Tickets = new HashSet<Tickets>();
        }

        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AppointmentPrice> AppointmentPrice { get; set; }
        public virtual ICollection<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class AppointmentPrice
    {
        public int AppointmentPriceId { get; set; }
        public int SupplierId { get; set; }
        public int Price { get; set; }
        public int Commision { get; set; }
        public int AppointmentId { get; set; }

        public virtual Appointments Appointment { get; set; }
        public virtual Suppliers Supplier { get; set; }
    }
}

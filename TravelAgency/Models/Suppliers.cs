using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            AppointmentPrice = new HashSet<AppointmentPrice>();
            Tickets = new HashSet<Tickets>();
        }

        public int SupplierId { get; set; }
        public int Commision { get; set; } 
        public int SupplierOrder { get; set; }
        public string FullName { get; set; }
        public string Adreess1 { get; set; }
        public string Phone1 { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AppointmentPrice> AppointmentPrice { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}

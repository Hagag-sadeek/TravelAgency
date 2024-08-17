using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int CustomerId { get; set; }

        public string Code { get; set; }
        public string Job { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Adreess1 { get; set; }
        public string Adreess2 { get; set; }
        [Required]
        [MaxLength(11), MinLength(11)]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public bool IsActive { get; set; } = true;
        public bool SendWhatsApp { get; set; } = true;
        public bool NeedUpdate { get; set; } = false;
        public bool Updated { get; set; } = false;

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public int BranchId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public int SupplierId { get; set; }
        public virtual Branches Branch { get; set; }
        public virtual Suppliers Supplier { get; set; }
    }
}

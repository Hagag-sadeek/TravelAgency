using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public partial class AppointmentDetails
    {
        public int AppointmentDetailId { get; set; }
        public int? AppointmentId { get; set; }
        public int BranchId { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public int Price { get; set; }

        public virtual Appointments Appointment { get; set; }
        public virtual Branches Branch { get; set; }
    }
}

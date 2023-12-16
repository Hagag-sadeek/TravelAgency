using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public partial class AppointmentBusView
    {
        [Key]
        public int AppointmentBusViewtId { get; set; }

        public int AppointmentId { get; set; }
        public string ViewName { get; set; }

        public DateTime TicketDate { get; set; }

    }

}

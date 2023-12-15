using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public partial class AppointmentBusView
    {
        [Key]
        public int AppointmentId { get; set; }
        public string ViewName { get; set; }

    }

}

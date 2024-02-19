using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public partial class UserAppointments
    {
        [Key]
        public int Id { get; set; }

        public int AppId { get; set; }

        public int UserId { get; set; }
    }

}

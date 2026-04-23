using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Core.Entities
{
    public partial class UserAppointments
    {
        [Key]
        public int Id { get; set; }

        public int AppId { get; set; }

        public int UserId { get; set; }
    }

}

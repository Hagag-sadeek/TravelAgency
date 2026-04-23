using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Appointments
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "العنوان مطلوب")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int SortOrder { get; set; }
    }
}

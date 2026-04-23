namespace TravelAgency.Application.DTOs.Appointments
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }
    }
}

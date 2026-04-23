using System;

namespace TravelAgency.Application.DTOs.AppointmentDetails
{
    public class AppointmentDetailDto
    {
        public int AppointmentDetailId { get; set; }
        public int? AppointmentId { get; set; }
        public string? AppointmentTitle { get; set; }
        public int BranchId { get; set; }
        public string? BranchTitle { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public int Price { get; set; }
    }
}

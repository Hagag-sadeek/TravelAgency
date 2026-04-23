using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.AppointmentDetails
{
    public class CreateAppointmentDetailDto
    {
        [Required(ErrorMessage = "الميعاد مطلوب")]
        public int? AppointmentId { get; set; }

        [Required(ErrorMessage = "الفرع مطلوب")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "وقت المغادرة مطلوب")]
        public TimeSpan LeaveTime { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0, int.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public int Price { get; set; }
    }
}

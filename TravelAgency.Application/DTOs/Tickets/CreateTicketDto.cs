using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Tickets
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage = "العميل مطلوب")]
        public int? CustomerId { get; set; }

        public int? SupplierId { get; set; }

        [Required(ErrorMessage = "الميعاد مطلوب")]
        public int AppointmentId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاريخ الحجز مطلوب")]
        public DateTime TicketDate { get; set; }

        [Range(0, 500, ErrorMessage = "السعر يجب أن يكون بين 0 و 500")]
        [Required(ErrorMessage = "السعر مطلوب")]
        public int Price { get; set; }

        public int? FromBranchId { get; set; }

        public int? ToBranchId { get; set; }

        [Required(ErrorMessage = "رقم المقعد مطلوب")]
        [Range(1, 50, ErrorMessage = "رقم المقعد يجب أن يكون بين 1 و 50")]
        public int SeatId { get; set; }

        public string? Comment { get; set; }

        public bool IsFemale { get; set; }

        public bool IsCairo { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.ViewModels
{
    public partial class TicketViewModel
    {
        public TicketViewModel()
        {
            reservedTickets = new List<ReservedTickets>();
        }

        public int TicketId { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "من فضلك ادخل ميعاد الحجز")]
        public DateTime TicketDate { get; set; }

        [Range(0, 500, ErrorMessage = "من فضلك ادخل سعر التذكره")]
        [Required(ErrorMessage = "من فضلك ادخل سعر التذكره")]
        public int Price { get; set; }

        public int? FromBranchId { get; set; }
        public int? ToBranchId { get; set; }

        [Range(0, 50, ErrorMessage = "من فضلك اختر رقم المقعد")]
        public int SeatId { get; set; }

        public string comment { get; set; }

        public bool IsCairo { get; set; }

        ///////DropDowns
        public SelectList AppointmentsList { get; set; }
        public SelectList CustomersList { get; set; }
        public SelectList BranchsList { get; set; }
        public SelectList SuppliersList { get; set; }

        public List<ReservedTickets> reservedTickets { get; set; }
    }

    public partial class MoreTicketViewModel
    {

        public int TicketId { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "من فضلك ادخل ميعاد الحجز")]
        public DateTime TicketDate { get; set; }

        [Range(0, 500, ErrorMessage = "من فضلك ادخل سعر التذكره")]
        [Required(ErrorMessage = "من فضلك ادخل سعر التذكره")]
        public int Price { get; set; }

        public int? FromBranchId { get; set; }
        public int? ToBranchId { get; set; }

        public string SeatId { get; set; }

        public string comment { get; set; }

        public bool IsCairo { get; set; }

        ///////DropDowns
        public SelectList AppointmentsList { get; set; }
        public SelectList CustomersList { get; set; }
        public SelectList BranchsList { get; set; }
        public SelectList SuppliersList { get; set; }

    }
}




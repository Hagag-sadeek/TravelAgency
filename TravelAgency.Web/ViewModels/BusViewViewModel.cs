using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Web.ViewModels
{
    public partial class BusViewViewModel
    {

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "?? ???? ???? ?????")]
        public DateTime TicketDate { get; set; }

        [Required(ErrorMessage = "?? ???? ???? ???????")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "?? ???? ???? ???????")]
        public int ViewNameId { get; set; }



        public SelectList AppointmentsList { get; set; }
        public SelectList ViewNameList { get; set; }

    }
}

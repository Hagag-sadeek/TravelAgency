using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;

namespace TravelAgency.ViewModels
{
    public partial class BusViewViewModel
    {

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "من فضلك ادخل اليوم")]
        public DateTime TicketDate { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل الميعاد")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل الترقيم")]
        public int ViewNameId { get; set; }

        

        public SelectList AppointmentsList { get; set; } 
        public SelectList ViewNameList { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelAgency.ViewModels
{
    public class ReservedTicketsReport
    {

        public ReservedTicketsReport()
        {
            ReservedTicketsDetailsReport = new List<ReservedTicketsDetailsReport>();
        }

        public int AppointmentId { get; set; }

        [DataType(DataType.Date)]
        public DateTime TicketDate { get; set; }

        public List<ReservedTicketsDetailsReport> ReservedTicketsDetailsReport { get; set; }
        public SelectList AppointmentsList { get; set; }
    }


    public class ReservedTicketsDetailsReport
    {
        public string SupplierName { get; set; }

        public int TicketCount { get; set; }

        public int TotalIncome { get; set; }
        public int NetIncome { get; set; }
    }
}
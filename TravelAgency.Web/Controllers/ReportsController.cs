using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _reportService.GetActiveAppointmentsForReportAsync();

            var model = new ReservedTicketsReport()
            {
                TicketDate = DateTime.Now,
                AppointmentsList = new SelectList(appointments, "Id", "Title"),
                ReservedTicketsDetailsReport = new System.Collections.Generic.List<ReservedTicketsDetailsReport>()
            };

            return View(nameof(Create), model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ReservedTicketsReport model)
        {
            if (ModelState.IsValid)
            {
                var reportResult = await _reportService.GenerateTicketReportAsync(model.AppointmentId, model.TicketDate);

                // Map to ViewModel
                foreach (var detail in reportResult.SupplierDetails)
                {
                    model.ReservedTicketsDetailsReport.Add(new ReservedTicketsDetailsReport
                    {
                        SupplierName = detail.SupplierName,
                        TicketCount = detail.TicketCount,
                        TotalIncome = detail.TotalIncome,
                        NetIncome = detail.NetIncome
                    });
                }
            }

            var appointments = await _reportService.GetActiveAppointmentsForReportAsync();
            model.TicketDate = DateTime.Now;
            model.AppointmentsList = new SelectList(appointments, "Id", "Title");

            return View(model);
        }
    }
}

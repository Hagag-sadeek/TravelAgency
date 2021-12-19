using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TravelAgency.Models;
using TravelAgency.ViewModels;

namespace TravelAgency.Controllers
{
    public class ReportsController : Controller
    {
        private readonly TravelAgencyContext _context;

        public ReportsController(TravelAgencyContext context)
        {
            _context = context;
        }

 
        public IActionResult Index()
        {
            var model = new ReservedTicketsReport()
            {
                TicketDate = DateTime.Now,
                AppointmentsList =
                    new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title"),
                ReservedTicketsDetailsReport = new List<ReservedTicketsDetailsReport>() { }
            };

            return View(nameof(Create),model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create(ReservedTicketsReport model)
        {

            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
                {
                    return RedirectToAction("Login", "Account");
                }

                var rTickets = _context.Tickets
                    .Include(x => x.Customer)
                    .Include(x => x.Supplier)
                    .Include(x => x.FromBranch)
                    .Include(x => x.ToBranch)
                    .Where(x => x.AppointmentId == model.AppointmentId &&
                                x.TicketDate == model.TicketDate &&
                                x.IsActive)
                    .ToList();

                foreach (var item in rTickets.Select(x => x.SupplierId).Distinct().ToList())
                {
                    var supplier = _context.Suppliers.Find(item);
                    var ticketCount = rTickets.Count(x => x.SupplierId == item);
                   // var totalIncome = rTickets.Where(x => x.SupplierId == item).Sum(x => x.Price);
                   var list = new ReservedTicketsDetailsReport()
                   {
                       SupplierName = supplier.FullName,
                       TicketCount = ticketCount,
                       NetIncome = ticketCount * ((_context.AppointmentPrice.First(x =>
                                                          x.AppointmentId == model.AppointmentId &&
                                                          x.SupplierId == supplier.SupplierId)
                                                      .Price) - (_context.AppointmentPrice.First(x =>
                                                          x.AppointmentId == model.AppointmentId &&
                                                          x.SupplierId == supplier.SupplierId)
                                                      .Commision)),
                       TotalIncome = ticketCount * (_context.AppointmentPrice.First(x =>
                                             x.AppointmentId == model.AppointmentId &&
                                             x.SupplierId == supplier.SupplierId)
                                         .Price) 
                   };
                    model.ReservedTicketsDetailsReport.Add(list);
                }
                 
            }

            model.TicketDate = DateTime.Now;
            model.AppointmentsList =
                new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title");

            return View(model);
        }
    }
}

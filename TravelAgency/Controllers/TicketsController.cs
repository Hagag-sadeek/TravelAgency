using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Helper;
using TravelAgency.Models;
using TravelAgency.ViewModels;
 
namespace TravelAgency.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TravelAgencyContext _context;
    
        public TicketsController(TravelAgencyContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var travelAgencyContext = _context.Tickets
                .Include(t => t.Appointment)
                .Include(t => t.Customer)
                .Include(t => t.FromBranch)
                .Include(t => t.Supplier)
                .Include(t => t.ToBranch);

            return View(await travelAgencyContext.ToListAsync());
        }

        #region CreateAdmin

        [Route("CreateAdmin")] 
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new TicketViewModel
            {
                AppointmentsList =
                    new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title"),
                CustomersList = new SelectList(_context.Customers.Where(x => x.IsActive), "CustomerId", "FullName"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive), "SupplierId", "FullName"),
                TicketDate = DateTime.Now.Date
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateAdmin")]
        public IActionResult CreateAdmin( Tickets tickets)
        {

            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                 
                if (tickets.SeatId <= 0|| TicketsExists(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId))
                    return View(nameof(CreateAdmin), PopulateReserveViewModel(tickets));

                if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
                {
                    return RedirectToAction("Login", "Account");
                }

                tickets.UserId = HttpContext.Session.GetInt32("UserId").Value;

                //  tickets.Price = _context.AppointmentPrice.First(e =>
                //    e.AppointmentId == tickets.AppointmentId && e.SupplierId == tickets.SupplierId).Price;

                //if (TicketsExists(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId))
                //{
                //    var ticketToUpdate = _context.Tickets.First(e => e.SeatId == tickets.SeatId && e.TicketDate.Date == tickets.TicketDate.Date);
                //    ticketToUpdate.Price = tickets.Price;
                //}
                //else
                //{
                //    _context.Add(tickets);
                //}
                tickets.IsActive = true;
                tickets.ReserveDate = DateTime.Now;
                _context.Tickets.Add(tickets);
                 _context.SaveChanges();
            }

            return View(nameof(CreateAdmin), PopulateReserveViewModel(tickets));
        }

        #endregion

        #region CreateNotAdmin
        [HttpGet]
        [Route("CreateNotAdmin")]
        public IActionResult CreateNotAdmin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new TicketViewModel()
            {
                AppointmentsList = new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title"),
                CustomersList = new SelectList(_context.Customers.Where(x => x.IsActive), "CustomerId", "FullName"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive), "SupplierId", "FullName")
            };
            model.TicketDate = DateTime.Now.Date;
 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateNotAdmin")]
        public IActionResult CreateNotAdmin(Tickets tickets, string name, string phone)
        {

            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (tickets.SeatId <= 0 || tickets.SeatId > 50  ||
                tickets.TicketDate.Date < DateTime.Now.Date ||
                TicketsExists(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId))

                return View(nameof(CreateNotAdmin), PopulateReserveViewModel(tickets));


            tickets.UserId = HttpContext.Session.GetInt32("UserId").Value;
            tickets.IsActive = true;
            tickets.ReserveDate=DateTime.Now;
        
            _context.Tickets.Add(tickets);

            _context.SaveChanges();

            return View(nameof(CreateNotAdmin), PopulateReserveViewModel(tickets));
        }

        #endregion

        #region ShowTickets 
        public IActionResult ShowTickets(Tickets model)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
          
            return View(nameof(CreateNotAdmin), PopulateReserveViewModel(model));
        }
         
        public IActionResult ShowTicketsForAdmin(Tickets model)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            //if (model.TicketDate < DateTime.Now)
            //    return RedirectToAction(nameof(Create)); 

            return View(nameof(CreateAdmin), PopulateReserveViewModel(model));
        }
        
        #endregion

        #region Delete & confirm

        public JsonResult DeleteTicket(int id)
        {

            var ticket = _context.Tickets.FirstOrDefault(x => x.TicketId == id);

            if (new SessionInfoSetup().IsAdmin() != "True")
            {
                return Json(false);
            }

            if (ticket == null)
                return Json(false);

            ticket.IsActive = false;
            ticket.Comment = "Deleted by" + _context.Users.Find(HttpContext.Session.GetInt32("UserId")).Firstname +
                             DateTime.Now;

            if (_context.SaveChanges() > 0)
                return Json(true);

            return Json(false);
        }

        public JsonResult ConfirmTicket(int id,string price)
        {

            var ticket = _context.Tickets.FirstOrDefault(x => x.TicketId == id);
 
            if (ticket == null)
                return Json(false);

            ticket.Price = Convert.ToInt32(price);
            _context.Tickets.Update(ticket);

            if (_context.SaveChanges() > 0)
                return Json(true);

            return Json(false);
        }
        

        #endregion

        private bool TicketsExists(int seatNum, DateTime date, int AppointmentId)
        {
            return _context.Tickets.Any(e =>
                e.SeatId == seatNum &&
                e.TicketDate.Date == date.Date &&
                e.AppointmentId == AppointmentId &&
                e.IsActive);
        }

        public TicketViewModel PopulateReserveViewModel(Tickets model)
        {
            var CurrentUserTypeId = new SessionInfoSetup().IsAdmin();

            var UserId = HttpContext.Session.GetInt32("UserId").Value;

            var Rtickets = _context.Tickets
                .Include(x => x.Customer)
                .Include(x => x.Supplier)
                .Include(x => x.FromBranch)
                .Include(x => x.ToBranch)
                .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate && x.IsActive)
                .ToList();

            var Vmodel = new TicketViewModel()
            {
                AppointmentsList =
                    new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title"),
                CustomersList = new SelectList(_context.Customers.Where(x => x.IsActive), "CustomerId", "FullName"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive), "SupplierId", "FullName")
            };
            if (Rtickets != null)
            {
                foreach (var item in Rtickets)
                {
                    var list = new ReservedTickets()
                    {
                        TicketId = item.TicketId,
                        Customer = item.Customer.FullName,
                        Supplier = item.Supplier.FullName,
                        Phone = item.Customer.Phone1,
                        SeatId = item.SeatId,
                        FromBranch = item.FromBranch.Title,
                        ToBranch = item.ToBranch.Title,
                        IsPaid = item.Price != 0,
                        Price = item.Price,
                        IsMine = ((item.SupplierId == _context.Users.Find(UserId).SupplierId) ||
                                  (CurrentUserTypeId == "True") || item.UserId == UserId)
                    };


                    Vmodel.reservedTickets.Add(list);
                }
            }

            return Vmodel;
        }

        #region Customers
        [HttpPost]
        public IActionResult AddCustomer(string name, string phone)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone))
                return RedirectToAction(nameof(CreateNotAdmin));

            var customer = _context.Customers.FirstOrDefault(x => x.Phone1 == phone.Trim() && x.IsActive);
            if (customer != null)
                customer.FullName = name.Trim();
            else
                _context.Customers.Add(new Customers() {FullName = name.Trim(), Phone1 = phone.Trim(), IsActive = true});

            _context.SaveChanges();
            return RedirectToAction(nameof(CreateNotAdmin));
        }

        [HttpPost]
        public IActionResult AddCustomerAdmin(string name, string phone)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone))
                return RedirectToAction(nameof(CreateAdmin));

            var customer = _context.Customers.FirstOrDefault(x => x.Phone1 == phone.Trim() && x.IsActive);
            if (customer != null)
                customer.FullName = name.Trim();
            else
                _context.Customers.Add(new Customers() { FullName = name.Trim(), Phone1 = phone.Trim(), IsActive = true });

            _context.SaveChanges();
            return RedirectToAction(nameof(CreateAdmin));
        }

        [HttpPost]
        public IActionResult FindCustomerByPhoneId(  string Phone)
        {
            var ticket = _context.Customers.FirstOrDefault(x => x.Phone1 == Phone && x.IsActive);

            if(ticket==null)
                return Json(-1);

            return Json(ticket.CustomerId);
        }
        #endregion

        #region MyTickets

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("MyTickets")]
        public IActionResult MyTickets( )
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(nameof(MyTickets), PopulateReserveViewModel(new Tickets()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("MyTickets")]
        public IActionResult MyTickets(Tickets model)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(nameof(MyTickets), PopulateReserveViewModel(model));
        }

        #endregion

        #region DOConfirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DoConfirmReserve")]
        public IActionResult DoConfirmReserve(Tickets model, int confirmPrice)
        {

            var ticket = _context.Tickets.Find(model.TicketId);
            ticket.Price = _context.AppointmentPrice.First(x =>
                x.SupplierId == ticket.SupplierId && x.AppointmentId == ticket.AppointmentId).Price;
            _context.Update(ticket);
            _context.SaveChanges();
            return View(nameof(CreateNotAdmin), PopulateReserveViewModel(model));
        }
        #endregion



        #region CreateMORE
        [HttpGet]
        [Route("CreateMore")]
        public IActionResult CreateMore()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new MoreTicketViewModel()
            {
                AppointmentsList = new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title"),
                CustomersList = new SelectList(_context.Customers.Where(x => x.IsActive), "CustomerId", "FullName"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive), "SupplierId", "FullName")
            };
            model.TicketDate = DateTime.Now.Date;

            var BranchId = _context.Users.Find(HttpContext.Session.GetInt32("UserId")).BranchId; 

            model.IsCairo = (BranchId == 1 || BranchId == 6);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateMore")]
        public async Task<IActionResult> CreateMore(MoreTicketViewModel tickets, string name, string phone)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone))
                tickets.CustomerId = 20;

            if (HttpContext.Session.GetInt32("UserId") == null
                            || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (tickets.TicketDate < DateTime.Now.Date  )
                    return RedirectToAction(nameof(CreateNotAdmin));
             

                if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(phone))
                {
                    var cus = _context.Customers.FirstOrDefault(x => x.IsActive && x.Phone1 == phone);
                    if (cus != null)
                    {
                        cus.FullName = name;
                        _context.SaveChanges();
                        tickets.CustomerId = cus.CustomerId;
                    }
                    else
                    {
                        _context.Customers.Add(new Customers()
                        {
                            FullName = name.Trim(),
                            Phone1 = phone.Trim(),
                            IsActive = true
                        });
                        _context.SaveChanges();
                        tickets.CustomerId = _context.Customers.First(x => x.IsActive && x.Phone1 == phone)
                            .CustomerId;
                    }
                }

                tickets.UserId = HttpContext.Session.GetInt32("UserId").Value;
                tickets.FromBranchId = _context.Users.Find(tickets.UserId).BranchId;

                if (tickets.SupplierId == null)
                    tickets.SupplierId = _context.Users.Find(tickets.UserId).SupplierId;

                if (tickets.ToBranchId == null)
                    tickets.ToBranchId = 1;

                tickets.Price = _context.AppointmentPrice.First(e =>
                    e.AppointmentId == tickets.AppointmentId && e.SupplierId == tickets.SupplierId).Price;

                _context.Add(tickets);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(CreateNotAdmin));
        }

        #endregion


        [HttpGet]
        public JsonResult  Fix()
        {
            var cus = _context.Customers.ToList();

            foreach (var item in cus)
            {
                var red = _context.Customers.Where(x => x.Phone1 == item.FullName && x.CustomerId != item.CustomerId).ToList();
                if (red != null)
                {
                    foreach (var xx in red)
                    {
                        xx.IsActive = false;
                    }
                    _context.SaveChanges();
                }
            }



            return Json(1);
        }
    }
}

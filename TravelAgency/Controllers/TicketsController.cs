using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using RestSharp;
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

            var currentUserId = HttpContext.Session.GetInt32("UserId").Value;

            var currentApps = _context.UserAppointments.Where(x => x.UserId == currentUserId).Select(x => x.AppId).ToList();
            if (currentApps.Count == 0)
                currentApps = _context.Appointments.Select(x => x.AppointmentId).ToList();

            var model = new TicketViewModel
            {
                AppointmentsList =
                    new SelectList(_context.Appointments.OrderBy(x => x.SortOrder).Where(x => x.IsActive && currentApps.Contains(x.AppointmentId) ).OrderBy(x => x.SortOrder)
                    , "AppointmentId", "Title"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive).OrderBy(x => x.BranchOrder), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive).OrderBy(x => x.SupplierOrder), "SupplierId", "FullName"),
                TicketDate = DateTime.Now.Date
            };

            var viewName = "CreateAdmin";
            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateAdmin5";

            return View(viewName, model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateAdmin")]
        public IActionResult CreateAdmin(Tickets tickets)
        {

            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {

                if (tickets.SeatId <= 0 ||
                    (TicketsExists(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId) ||
                    tickets.CustomerId == null))
                    return View(nameof(CreateAdmin), PopulateReserveViewModel(tickets));

                if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
                {
                    return RedirectToAction("Login", "Account");
                }

                tickets.UserId = HttpContext.Session.GetInt32("UserId").Value;

                tickets.IsActive = true;
                tickets.ReserveDate = DateTime.Now;
                _context.Tickets.Add(tickets);
                _context.SaveChanges();
            }

            var viewName = "CreateAdmin";
            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == tickets.AppointmentId && x.TicketDate == tickets.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateAdmin5";

            try
            {
                if (TicketsExistsForThisCustomer(tickets.CustomerId.Value, tickets.TicketDate.Date, tickets.AppointmentId) )

                    sendWhatsAppNotifications(_context.Customers.Find(tickets.CustomerId).Phone1, tickets.SeatId,
                        tickets.TicketDate, _context.Suppliers.Find(tickets.SupplierId).Adreess1, viewName);
            }
            catch (Exception ex) { }

            return View(viewName, PopulateReserveViewModel(tickets));
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


            var currentUserId = HttpContext.Session.GetInt32("UserId").Value;

            var currentApps = _context.UserAppointments.Where(x => x.UserId == currentUserId).Select(x => x.AppId).ToList();
            if (currentApps.Count == 0)
                currentApps = _context.Appointments.Select(x => x.AppointmentId).ToList();

            var model = new TicketViewModel()
            {
                AppointmentsList = new SelectList(_context.Appointments.OrderBy(x => x.SortOrder).Where(x => x.IsActive && currentApps.Contains(x.AppointmentId)), "AppointmentId", "Title"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive), "SupplierId", "FullName"),
                TicketDate = DateTime.Now
            };

            var viewName = "CreateNotAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateNotAdmin5";

            return View(viewName, model);
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

            if (tickets.SeatId <= 0 || tickets.SeatId > 50 ||
                tickets.TicketDate.Date < DateTime.Now.Date ||
                TicketsExists(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId) ||
                    tickets.CustomerId == null)
                return View(nameof(CreateNotAdmin), PopulateReserveViewModel(tickets));


            tickets.UserId = HttpContext.Session.GetInt32("UserId").Value;
            tickets.IsActive = true;
            tickets.ReserveDate = DateTime.Now;

            _context.Tickets.Add(tickets);

            _context.SaveChanges();

            var viewName = "CreateNotAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == tickets.AppointmentId && x.TicketDate == tickets.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateNotAdmin5";


            return View(viewName, PopulateReserveViewModel(tickets));
        }

        #endregion

        #region ShowTickets 

        public IActionResult ShowTicketsForAdmin(Tickets model)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            var viewName = "CreateAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateAdmin5";

            return View(viewName, PopulateReserveViewModel(model));
        }

        public IActionResult ShowTickets(Tickets model)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            var CurrentUserTypeId = new SessionInfoSetup().IsAdmin();
            if (model.TicketDate.Date <  DateTime.Now.Date && CurrentUserTypeId == "False")
            {
                model.TicketDate = DateTime.Now.Date;
            }

            var viewName = "CreateNotAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateNotAdmin5";


            return View(viewName, PopulateReserveViewModel(model));
        }

        #endregion

        #region Delete & confirm

        public JsonResult DeleteTicket(int id)
        {

            var ticket = _context.Tickets.FirstOrDefault(x => x.TicketId == id);

            //sendWhatsAppNotificationsWithCancell(_context.Customers.Find(ticket.CustomerId).Phone1, ticket.SeatId,
            //       ticket.TicketDate, _context.Suppliers.Find(ticket.SupplierId).Adreess1);

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

        public JsonResult ConfirmTicket(int id, string price)
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
                _context.Customers.Add(new Customers() { FullName = name.Trim(), Phone1 = phone.Trim(), IsActive = true });

            _context.SaveChanges();
            return RedirectToAction(nameof(CreateNotAdmin));
        }

        [HttpPost]
        public IActionResult AddCustomerAdmin(string name, string phone, string Adreess1)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone) || phone.Length != 11)
                return RedirectToAction(nameof(CreateAdmin));

            var nCustomer = new Customers();

            var customer = _context.Customers.FirstOrDefault(x => x.Phone1 == phone.Trim() && x.IsActive);
            if (customer != null)
            {
                customer.FullName = name.Trim();
                customer.Adreess1 = Adreess1;
            }
            else
            {
                nCustomer.FullName = name.Trim();
                nCustomer.Phone1 = phone.Trim();
                nCustomer.IsActive = true;
                nCustomer.Adreess1 = Adreess1;
               // nCustomer.Code = Convert.ToInt32(_context.Customers.OrderBy(x => x.CustomerId).Last().Code + 1).ToString();
                _context.Customers.Add(nCustomer);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(CreateAdmin));
        }

        [HttpPost]
        public IActionResult FindCustomerByPhoneId(string Phone)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Phone1.Contains(Phone) && x.IsActive);

            if (customer != null)
            {
                var x = customer.CustomerId + "&&" + customer.FullName + "&&" + customer.Phone1;
                return Json(x);
            }

            return Json("غير موجود");
        }

        [HttpPost]
        public IActionResult FindCustomerByPhoneIdForNotAdmin(string Phone)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Phone1 == Phone && x.IsActive);

            if (customer != null)
                return Json(customer.CustomerId);

            return Json(-1);
        }
        #endregion

        #region MyTickets

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("MyTickets")]
        public IActionResult MyTickets()
        {

            return View(nameof(MyTickets), PopulateReserveViewModel(new Tickets()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("MyTickets")]
        public IActionResult MyTickets(Tickets model)
        {

            var viewName = "MyTickets";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();

            else
                viewName = "MyTickets5";

            return View(viewName, PopulateReserveViewModel(model));
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

            var viewName = "CreateNotAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateNotAdmin5";

            return View(viewName, PopulateReserveViewModel(model));
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
                AppointmentsList = new SelectList(_context.Appointments.OrderBy(x => x.SortOrder).Where(x => x.IsActive), "AppointmentId", "Title"),
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
                if (tickets.TicketDate < DateTime.Now.Date)
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

        #region setBusView

        [Route("BusView")]
        [HttpGet]
        public IActionResult BusView()
        {

            var model = new BusViewViewModel
            {
                AppointmentsList = new SelectList(_context.Appointments.OrderBy(x => x.SortOrder).Where(x => x.IsActive), "AppointmentId", "Title"),
                TicketDate = DateTime.Now
            };

            return View("BusView", model);
        }


        [Route("BusView")]
        [HttpPost]
        public IActionResult BusView(BusViewViewModel model)
        {
            var row = _context.AppointmentBusView
                .Where(x => x.AppointmentId == model.AppointmentId && x.TicketDate == model.TicketDate.Date).ToList();

            _context.RemoveRange(row);

            var AppointmentBusView = new AppointmentBusView()
            {
                AppointmentId = model.AppointmentId,
                TicketDate = model.TicketDate.Date,
                ViewName = model.ViewNameId.ToString()
            };

            _context.Add(AppointmentBusView);

            _context.SaveChanges();

            return RedirectToAction(nameof(BusView));
        }
        #endregion

        #region Others 
        private bool TicketsExists(int seatNum, DateTime date, int AppointmentId)
        {
            return _context.Tickets.Any(e =>
                e.SeatId == seatNum &&
                e.TicketDate.Date == date.Date &&
                e.AppointmentId == AppointmentId &&
                e.IsActive);
        }

        private bool TicketsExistsForThisCustomer(int customerId, DateTime date, int AppointmentId)
        {
            var x = _context.Tickets.Count(e =>
                e.CustomerId == customerId &&
                e.TicketDate.Date == date.Date &&
                e.AppointmentId == AppointmentId &&
                e.IsActive);

            return x == 1;
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

            var currentUserId = HttpContext.Session.GetInt32("UserId").Value;

            var currentApps = _context.UserAppointments.Where(x => x.UserId == currentUserId).Select(x => x.AppId).ToList();
            if (currentApps.Count == 0)
                currentApps = _context.Appointments.Select(x => x.AppointmentId).ToList();

            var Vmodel = new TicketViewModel()
            {
                AppointmentsList =
                    new SelectList(_context.Appointments.OrderBy(x => x.SortOrder).Where(x => x.IsActive && currentApps.Contains(x.AppointmentId)), "AppointmentId", "Title"),
                CustomersList = new SelectList(_context.Customers.Where(x => x.IsActive), "CustomerId", "FullName"),
                BranchsList = new SelectList(_context.Branches.Where(x => x.IsActive).OrderBy(x=>x.BranchOrder), "BranchId", "Title"),
                SuppliersList = new SelectList(_context.Suppliers.Where(x => x.IsActive).OrderBy(x => x.SupplierOrder), "SupplierId", "FullName"),
                TicketDate = model.TicketDate.Date
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
                        IsMine = (item.SupplierId == _context.Users.Find(UserId).SupplierId)
                        || item.FromBranchId == _context.Users.Find(UserId).BranchId ||
                                  (CurrentUserTypeId == "True") || item.UserId == UserId
                    };


                    Vmodel.reservedTickets.Add(list);
                }
            }

            return Vmodel;
        }
        public IActionResult DontSentAgain(string phone)
        {

            var cust = _context.Customers.FirstOrDefault(x => x.Phone1 == phone);

            if (cust == null)
                return Json(false);

            cust.Phone3 = "Dont";
            _context.Customers.Update(cust);

            return View();
        }

        public string GetViewName(int AppointmentId, DateTime date)
        {
            var viewName = "CreateNotAdmin";

            var row = _context.AppointmentBusView
               .Where(x => x.AppointmentId == AppointmentId && x.TicketDate == date.Date).FirstOrDefault();

            if (row != null)
                viewName += row.ViewName.ToString();
            else
                viewName = "CreateNotAdmin5";


            return viewName;

        }
        #endregion

        #region send_Whatsapp
        private async void sendWhatsAppNotifications(string number, int seatNumber,DateTime tDate,string from,string viewName)
        {
            //var Window4=new List<int>() {1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33,35,39,37,43,41 };
            //var Window5 = new List<int>() { 1,4,5,8,9,12,13,16,17,20,21,23,25,28,29,32,33,36,37,40,41,44 };
            //var dir = "";

            //if (seatNumber == 50)
            //    dir = "مشرف";
            //else if ((new List<int>() { 45, 46, 47, 48, 49 }).Contains(seatNumber))
            //    dir = "كنبه";
            //else
            //{
            //    if (viewName == "CreateAdmin5")
            //        dir = Window5.Contains(seatNumber) ? "شــباك" : "مــمر";

            //    if (viewName == "CreateAdmin4")
            //        dir = Window4.Contains(seatNumber) ? "شــباك" : "مــمر";
            //}

            var url = "https://api.ultramsg.com/instance95337/messages/chat";
            var client = new RestClient(url);

            var request = new RestRequest(url, RestSharp.Method.Post);
            request.AddHeader("content-type", "application/json");

            var msg = "-مرحباً بحضرتك في شركه فـــوربـــاص للنقل البــــري وشكرا جزيلا لاخـتـيـارك لنا ولـثـقـتـك بـنـا ❤️.";
            msg += "\n\n";

           
            msg += "-الرجاء تسجيل هذا الرقم علي الهاتف ليصلك كل عروض ومستجدات العمل عبر منصه واتس اب";
            msg += "\n";
            msg += "فورباص القاهره الرئيسي";
            msg += "\n";
            msg += "01030565720";
            msg += "\n\n";

            msg += "-تفاصيل حجز حضرتك :";
            msg += "\n";
            msg += "يوم : "+ tDate.ToString("ddd", new CultureInfo("ar-BH")) + " - "+ tDate.ToShortDateString();
            msg += "\n";
            msg += "مــن : " + from;
            //msg += "\n";
            //msg += "كرسي رقم: " + seatNumber.ToString() + " - " + dir;
             
            msg += "\n\n";
            msg += "-الرجاء في حاله الغاء التذكره الاتصال بالمكتب قبل الميعاد بالوقت الكافي ";
            msg += "\n\n";

            //msg += "-  الرجاء لايك ودعم ومتابعه الصفحه الخاصه بالشركه عبر الفيس بوك🙏";
            //msg += "\n";
            //msg += "https://www.facebook.com/4BusEgypt?mibextid=ZbWKwL";
           
            msg += "-الرجاء في حاله وجود اي ملاحظه سواء من المكاتب او السائقين او الباصات الاتصال علي";
            msg += "\n";
            msg += "01030565720";

            

            var body = new
            {
                token = "516itsp3id9b8w0k",
                to = "+2" + number,
                body = msg
            };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            return;
        }

        private async void sendWhatsAppNotificationsWithCancell(string number, int seatNumber, DateTime tDate, string from)
        {


            var url = "https://api.ultramsg.com/instance1598/messages/chat";
            var client = new RestClient(url);

            var request = new RestRequest(url, RestSharp.Method.Post);
            request.AddHeader("content-type", "application/json");

            var msg = "-شكرا جزيلا لاخـتـيـارك لنا ولـثـقـتـك بـنـا ❤️";
            msg += "\n\n";

            msg += "- تـــم الــغــــاء حـــجـــز حـضــرتــك :";
            msg += "\n";
            msg += "يوم : " + tDate.ToString("ddd", new CultureInfo("ar-BH")) + " - " + tDate.ToShortDateString();
            msg += "\n";
            msg += "مــن : " + from;
            msg += "\n";
            msg += "كرسي رقم: " + seatNumber.ToString();
             

            var body = new
            {
                token = "p1b1f2225ezl9sps",
                to = "+2" + number,
                body = msg
            };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            return;
        }

        #endregion
    }
}

using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using TravelAgency.Helper;
using TravelAgency.Application.DTOs.Customers;
using TravelAgency.Application.DTOs.Tickets;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICustomerService _customerService;
        private readonly ISupplierService _supplierService;
        private readonly IAppointmentService _appointmentService;

        public TicketsController(
            ITicketService ticketService,
            ICustomerService customerService,
            ISupplierService supplierService,
            IAppointmentService appointmentService)
        {
            _ticketService = ticketService;
            _customerService = customerService;
            _supplierService = supplierService;
            _appointmentService = appointmentService;
        }

        #region Index

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsWithRelationsAsync();
            return View(tickets);
        }

        #endregion

        #region CreateAdmin

        [Route("CreateAdmin")]
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var currentUserId = GetCurrentUserId();
            var model = PopulateReserveViewModel(new Tickets { TicketDate = DateTime.Now.Date });
            var viewName = GetViewName(model.AppointmentId, model.TicketDate, true);

            return View(viewName, model);
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(Tickets tickets)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var viewName = GetViewName(tickets.AppointmentId, tickets.TicketDate, true);

            if (!ModelState.IsValid)
                return View(viewName, PopulateReserveViewModel(tickets));

            if (tickets.SeatId <= 0 || tickets.SeatId > 50 || 
                await _ticketService.IsSeatAvailableAsync(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId) == false || 
                tickets.CustomerId == null)
                return View(viewName, PopulateReserveViewModel(tickets));

            var createDto = new CreateTicketDto
            {
                CustomerId = tickets.CustomerId,
                SupplierId = tickets.SupplierId,
                AppointmentId = tickets.AppointmentId,
                TicketDate = tickets.TicketDate,
                Price = 0,
                FromBranchId = tickets.FromBranchId,
                ToBranchId = tickets.ToBranchId,
                SeatId = tickets.SeatId,
                Comment = tickets.Comment,
                IsFemale = tickets.IsFemale
            };

            await _ticketService.CreateTicketAsync(createDto, GetCurrentUserId());

            var cus = await _customerService.GetCustomerByIdAsync(tickets.CustomerId.Value);
            if (cus != null && await _ticketService.IsFirstTicketForCustomerAsync(tickets.CustomerId.Value, tickets.TicketDate.Date, tickets.AppointmentId))
            {
                SendWelcomeWhatsApp(cus.Phone1);
            }

            return View(viewName, PopulateReserveViewModel(tickets));
        }

        #endregion

        #region CreateNotAdmin

        [HttpGet]
        [Route("CreateNotAdmin")]
        public IActionResult CreateNotAdmin()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var model = PopulateReserveViewModel(new Tickets { TicketDate = DateTime.Now.Date });
            var viewName = GetViewName(model.AppointmentId, model.TicketDate, false);

            return View(viewName, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateNotAdmin")]
        public async Task<IActionResult> CreateNotAdmin(Tickets tickets)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var viewName = GetViewName(tickets.AppointmentId, tickets.TicketDate, false);

            var ticketsExist = !await _ticketService.IsSeatAvailableAsync(tickets.SeatId, tickets.TicketDate.Date, tickets.AppointmentId);

            if (tickets.SeatId <= 0 || tickets.SeatId > 50 || tickets.TicketDate.Date < DateTime.Now.Date || 
                ticketsExist || tickets.CustomerId == null)
                return View(viewName, PopulateReserveViewModel(tickets));

            var createDto = new CreateTicketDto
            {
                CustomerId = tickets.CustomerId,
                SupplierId = tickets.SupplierId,
                AppointmentId = tickets.AppointmentId,
                TicketDate = tickets.TicketDate,
                Price = tickets.Price,
                FromBranchId = tickets.FromBranchId,
                ToBranchId = tickets.ToBranchId,
                SeatId = tickets.SeatId,
                Comment = tickets.Comment,
                IsFemale = tickets.IsFemale
            };

            await _ticketService.CreateTicketAsync(createDto, GetCurrentUserId());

            var cus = await _customerService.GetCustomerByIdAsync(tickets.CustomerId.Value);
            if (cus != null && await _ticketService.IsFirstTicketForCustomerAsync(tickets.CustomerId.Value, tickets.TicketDate.Date, tickets.AppointmentId))
            {
                SendWelcomeWhatsApp(cus.Phone1);
            }

            return View(viewName, PopulateReserveViewModel(tickets));
        }

        #endregion

        #region ShowTickets

        public IActionResult ShowTicketsForAdmin(Tickets model)
        {
            if (model.TicketDate < DateTime.Now.Date && GetCurrentUserId() != 65)
                model.TicketDate = DateTime.Now.Date;

            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var viewName = GetViewName(model.AppointmentId, model.TicketDate, true);
            return View(viewName, PopulateReserveViewModel(model));
        }

        public IActionResult ShowTickets(Tickets model)
        {
            if (model.TicketDate < DateTime.Now.Date && GetCurrentUserId() != 65)
                model.TicketDate = DateTime.Now.Date;

            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var isAdmin = IsAdmin();
            if (model.TicketDate.Date < DateTime.Now.Date && !isAdmin)
                model.TicketDate = DateTime.Now.Date;

            var viewName = GetViewName(model.AppointmentId, model.TicketDate, false);
            return View(viewName, PopulateReserveViewModel(model));
        }

        #endregion

        #region Delete & Confirm

        public async Task<JsonResult> DeleteTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.DeleteTicketAsync(id, GetCurrentUserId());
            return Json(result);
        }

        public async Task<JsonResult> GetCustomerInfo(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var customerInfo = await _ticketService.GetCustomerInfoByTicketIdAsync(id);
            return Json(customerInfo ?? (object)false);
        }

        public async Task<JsonResult> ConfirmTicket(int id, string price)
        {
            if (!int.TryParse(price, out int priceValue))
                return Json(false);

            var result = await _ticketService.ConfirmTicketPriceAsync(id, priceValue);
            return Json(result);
        }

        #endregion

        #region Customers

        [HttpPost]
        public async Task<IActionResult> AddCustomer(string name, string phone)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
                return RedirectToAction(nameof(CreateNotAdmin));

            var dto = new QuickAddCustomerDto { Name = name, Phone = phone };
            await _customerService.QuickAddCustomerAsync(dto);

            return RedirectToAction(nameof(CreateNotAdmin));
        }

        [HttpPost]
        public async Task<JsonResult> AddCustomerAdmin(string name, string phone, string Adreess1)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || phone.Length != 11)
                return Json(false);

            var dto = new QuickAddCustomerDto { Name = name, Phone = phone };
            await _customerService.QuickAddCustomerAsync(dto);

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomerByPhoneId(string Phone)
        {
            var customer = await _customerService.GetCustomerByPhoneAsync(Phone);

            if (customer != null)
            {
                var result = $"{customer.CustomerId}&&{customer.FullName}&&{customer.Phone1}&&{customer.Code}&&{customer.Points}";
                return Json(result);
            }

            return Json("غير موجود");
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomerByCode(string code)
        {
            var customers = await _customerService.SearchCustomersAsync(code);
            var customer = customers.FirstOrDefault();

            if (customer != null)
            {
                var result = $"{customer.CustomerId}&&{customer.FullName}&&{customer.Phone1}&&{customer.Points}";
                return Json(result);
            }

            return Json("غير موجود");
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomerByPhoneIdForNotAdmin(string Phone)
        {
            var customer = await _customerService.GetCustomerByPhoneAsync(Phone);

            if (customer != null)
                return Json(customer.CustomerId);

            return Json(-1);
        }

        #endregion

        #region MyTickets

        [HttpGet]
        [Route("MyTickets")]
        public IActionResult MyTickets()
        {
            return View(nameof(MyTickets), PopulateReserveViewModel(new Tickets { TicketDate = DateTime.Now.Date }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("MyTickets")]
        public IActionResult MyTickets(Tickets model)
        {
            var viewName = GetViewName(model.AppointmentId, model.TicketDate, false);
            return View(viewName, PopulateReserveViewModel(model));
        }

        #endregion

        #region DOConfirm

        public async Task<JsonResult> PayTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.ConfirmTicketPriceAsync(id, 270);
            return Json(result);
        }

        public async Task<JsonResult> CancelPayTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.ConfirmTicketPriceAsync(id, 0);
            return Json(result);
        }

        public async Task<JsonResult> ConformTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.ConformTicketAsync(id);
            return Json(result);
        }

        public async Task<JsonResult> CancelConformTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.CancelConformTicketAsync(id);
            return Json(result);
        }

        #endregion

        #region CreateMore

        [HttpGet]
        [Route("CreateMore")]
        public async Task<IActionResult> CreateMore()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var appointments = await _appointmentService.GetAllActiveAppointmentsAsync();
            var customers = await _customerService.GetAllActiveCustomersAsync();
            var suppliers = await _supplierService.GetAllActiveSuppliersAsync();

            var model = new MoreTicketViewModel
            {
                AppointmentsList = new SelectList(appointments.OrderBy(x => x.SortOrder), "AppointmentId", "Title"),
                CustomersList = new SelectList(customers, "CustomerId", "FullName"),
                SuppliersList = new SelectList(suppliers, "SupplierId", "FullName"),
                TicketDate = DateTime.Now.Date
            };

            return View(model);
        }

        #endregion

        #region SetBusView

        [Route("BusView")]
        [HttpGet]
        public async Task<IActionResult> BusView()
        {
            var appointments = await _appointmentService.GetAllActiveAppointmentsAsync();

            var model = new BusViewViewModel()
            {
                AppointmentsList = new SelectList(appointments.OrderBy(x => x.SortOrder), "AppointmentId", "Title"),
                TicketDate = DateTime.Now
            };

            return View("BusView", model);
        }

        [Route("BusView")]
        [HttpPost]
        public async Task<IActionResult> BusView(BusViewViewModel model)
        {
            await _ticketService.SetBusViewAsync(model.AppointmentId, model.TicketDate.Date, model.ViewNameId.ToString());
            return RedirectToAction(nameof(BusView));
        }

        #endregion

        #region Helpers

        private bool TicketsExists(int seatNum, DateTime date, int appointmentId)
        {
            return !_ticketService.IsSeatAvailableAsync(seatNum, date, appointmentId).Result;
        }

        private bool TicketsExistsForThisCustomer(int customerId, DateTime date, int appointmentId)
        {
            return _ticketService.IsFirstTicketForCustomerAsync(customerId, date, appointmentId).Result;
        }

        public TicketViewModel PopulateReserveViewModel(Tickets model)
        {
            var currentUserId = GetCurrentUserId();
            var isAdmin = IsAdmin();

            var userAppointments = _ticketService.GetUserAppointmentsAsync(currentUserId).Result.ToList();
            var appointments = _appointmentService.GetAllActiveAppointmentsAsync().Result
                .Where(x => userAppointments.Any(ua => ua.Id == x.AppointmentId))
                .OrderBy(x => x.SortOrder);

            var customers = _customerService.GetAllActiveCustomersAsync().Result;
            var suppliers = _supplierService.GetAllActiveSuppliersAsync().Result;
            var reservedSeats = _ticketService.GetReservedSeatsAsync(model.AppointmentId, model.TicketDate, currentUserId, isAdmin).Result;

            var viewModel = new TicketViewModel
            {
                AppointmentsList = new SelectList(appointments, "AppointmentId", "Title"),
                CustomersList = new SelectList(customers, "CustomerId", "FullName"),
                SuppliersList = new SelectList(suppliers.OrderBy(s => s.SupplierOrder), "SupplierId", "FullName"),
                TicketDate = model.TicketDate.Date,
                reservedTickets = reservedSeats.Select(rs => new ReservedTickets
                {
                    TicketId = rs.TicketId,
                    Customer = rs.CustomerName,
                    Supplier = rs.SupplierName,
                    Phone = rs.Phone,
                    SeatId = rs.SeatId,
                    Code = rs.Code,
                    IsFemale = rs.IsFemale,
                    Price = rs.Price,
                    IsConformed = rs.IsConformed,
                    IsMine = rs.IsMine
                }).ToList()
            };

            return viewModel;
        }

        public IActionResult DontSentAgain(string phone)
        {
            var customer = _customerService.GetCustomerByPhoneAsync(phone).Result;
            if (customer != null)
            {
                // Update customer to not receive messages
                // This would need to be added to CustomerService
            }
            return View();
        }

        public string GetViewName(int appointmentId, DateTime date, bool isAdmin)
        {
            var baseViewName = isAdmin ? "CreateAdmin" : "CreateNotAdmin";
            var viewName = _ticketService.DetermineViewNameAsync(appointmentId, date, isAdmin).Result;
            return viewName;
        }

        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetInt32("UserId") > 0;
        }

        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }

        private bool IsAdmin()
        {
            return new SessionInfoSetup().IsAdmin() == "True";
        }

        #endregion

        #region WhatsApp Notifications

        private async void SendWelcomeWhatsApp(string number)
        {
            try
            {
                var url = "https://api.ultramsg.com/instance138410/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, RestSharp.Method.Post);
                request.AddHeader("content-type", "application/json");

                var msg = "-مرحباً بحضرتك في شركه فـــوربـــاص للنقل البــــري وشكرا جزيلا لاخـتـيـارك لنا ولـثـقـتـك بـنـا ❤️.";
                msg += "\n\n";
                msg += "-في حالة وجود أي استفسار أو تعديل، فريق فورباص دائماً في خدمتك 👍";
                msg += "\n\n";
                msg += "-وكمان من خلال خدمة (أي خدمة) تقدر تعتمد علينا في:";
                msg += "\n✔️ مشاوير\n✔️ تخليص أوراق\n✔️ توصيل طلبات\n✔️ حجز خدمات\n✔️ أي حاجة بدل ما تتعب ✨";
                msg += "\n\n";
                msg += "-نتمنى لحضرتك رحلة مريحة وسفراً سعيداً 🚍";

                var body = new
                {
                    token = "4eskefkg07hbwru8",
                    to = "+2" + number,
                    body = msg
                };
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                await client.ExecuteAsync(request);
            }
            catch { }
        }

        private async void SendWhatsAppNotifications(string number, int points, string code, int seatNumber, DateTime tDate, string from, string viewName)
        {
            try
            {
                var url = "https://api.ultramsg.com/instance138410/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, RestSharp.Method.Post);
                request.AddHeader("content-type", "application/json");

                var msg = "-مرحباً بحضرتك في شركه فـــوربـــاص للنقل البــــري وشكرا جزيلا لاخـتـيـارك لنا ولـثـقـتـك بـنـا ❤️.";
                msg += "\n\n";
                msg += $"-عد نقاطك : {points}\n";
                msg += $"- الكود الخاص بك داخل نظام الحجز الاليكتروني للشركه هو : {code}\n";
                msg += "- الان مع كل 50 نقطه تقدر تحصل علي 50 جنيه خصم علي سعر التذكره\n\n";
                msg += "-تفاصيل الحجز :\n";
                msg += $"يوم : {tDate.ToString("ddd", new CultureInfo("ar-BH"))} - {tDate.ToShortDateString()}\n";
                msg += $"مــن : {from}\n\n";
                msg += "-لارقام المكاتب والعناوين اضغط 1\n-للاسعار اضغط 2\n-للمواعيد اضغط 3\n-لموقع رمسيس على الخريطه اضغط 4\n-لموقع عين شمس على الخريطه اضغط 5";

                var body = new
                {
                    token = "4eskefkg07hbwru8",
                    to = "+2" + number,
                    body = msg
                };
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                await client.ExecuteAsync(request);
            }
            catch { }
        }

        private async void SendWhatsAppNotificationsWithCancell(string number, int seatNumber, DateTime tDate, string from)
        {
            try
            {
                var url = "https://api.ultramsg.com/instance95337/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, RestSharp.Method.Post);
                request.AddHeader("content-type", "application/json");

                var msg = "-مرحباً بحضرتك في شركه فـــوربـــاص للنقل البــــري وشكرا جزيلا لاخـتـيـارك لنا ولـثـقـتـك بـنـا ❤️.\n\n";
                msg += "- تـــم الــغــــاء حـــجـــز حـضــرتــك :\n";
                msg += $"يوم : {tDate.ToString("ddd", new CultureInfo("ar-BH"))} - {tDate.ToShortDateString()}\n";
                msg += $"مــن : {from}\n\n";
                msg += "لارقام المكاتب والعناوين اضغط 1\nللاسعار اضغط 2\nللمواعيد اضغط 3\nلموقع رمسيس على الخريطه اضغط 4\nلموقع عين شمس على الخريطه اضغط 5";

                var body = new
                {
                    token = "a516itsp3id9b8w0khhh",
                    to = "+2" + number,
                    body = msg
                };
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                await client.ExecuteAsync(request);
            }
            catch { }
        }

        private async void SendWhatsAppNotificationsWithPointsOnly(string number, int points)
        {
            try
            {
                var url = "https://api.ultramsg.com/instance95337/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, RestSharp.Method.Post);
                request.AddHeader("content-type", "application/json");

                var msg = $"-عد نقاطك : {points}\n";
                msg += "- الان مع كل 50 نقطه تقدر تحصل علي 50 جنيه خصم علي سعر التذكره";

                var body = new
                {
                    token = "a516itsp3id9b8w0khhh",
                    to = "+2" + number,
                    body = msg
                };
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                await client.ExecuteAsync(request);
            }
            catch { }
        }

        #endregion
    }
}

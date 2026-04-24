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
using TravelAgency.Web.ViewModels;

namespace TravelAgency.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICustomerService _customerService;
        private readonly ISupplierService _supplierService;

        public TicketsController(
            ITicketService ticketService,
            ICustomerService customerService,
            ISupplierService supplierService)
        {
            _ticketService = ticketService;
            _customerService = customerService;
            _supplierService = supplierService;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsWithRelationsAsync();
            return View(tickets);
        }

        #endregion

        #region CreateAdmin

        [Route("CreateAdmin")]
        [HttpGet]
        public async Task<IActionResult> CreateAdmin()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();

            // Get first appointment for initial load
            var appointments = await _ticketService.GetUserAppointmentsAsync(userId);
            var appointmentsList = appointments.ToList();

            if (!appointmentsList.Any())
            {
                return View("NoAppointments"); // or redirect to error page
            }

            var firstAppointmentId = appointmentsList.First().Id;

            var viewModel = await BuildReservationViewModelAsync(firstAppointmentId, DateTime.Now.Date, userId, true);
            var viewName = await _ticketService.DetermineViewNameAsync(firstAppointmentId, DateTime.Now.Date, true);

            return View(viewName, viewModel);
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(CreateTicketDto createDto)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();
            var viewName = await _ticketService.DetermineViewNameAsync(createDto.AppointmentId, createDto.TicketDate, true);

            if (!ModelState.IsValid || createDto.SeatId <= 0 || createDto.SeatId > 50 || createDto.CustomerId == null)
            {
                var viewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, true);
                return View(viewName, viewModel);
            }

            var isAvailable = await _ticketService.IsSeatAvailableAsync(createDto.SeatId, createDto.TicketDate, createDto.AppointmentId);
            if (!isAvailable)
            {
                var viewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, true);
                return View(viewName, viewModel);
            }

            createDto.Price = 0;
            await _ticketService.CreateTicketAsync(createDto, userId);

            var isFirstTicket = await _ticketService.IsFirstTicketForCustomerAsync(createDto.CustomerId.Value, createDto.TicketDate, createDto.AppointmentId);
            if (isFirstTicket)
            {
                var customer = await _customerService.GetCustomerByIdAsync(createDto.CustomerId.Value);
                if (customer != null && customer.Phone1 != null)
                    SendWelcomeWhatsApp(customer.Phone1);
            }

            var resultViewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, true);
            return View(viewName, resultViewModel);
        }

        #endregion

        #region CreateNotAdmin

        [HttpGet]
        [Route("CreateNotAdmin")]
        public async Task<IActionResult> CreateNotAdmin()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();

            // Get first appointment for initial load
            var appointments = await _ticketService.GetUserAppointmentsAsync(userId);
            var appointmentsList = appointments.ToList();

            if (!appointmentsList.Any())
            {
                return View("NoAppointments"); // or redirect to error page
            }

            var firstAppointmentId = appointmentsList.First().Id;

            var viewModel = await BuildReservationViewModelAsync(firstAppointmentId, DateTime.Now.Date, userId, false);
            var viewName = await _ticketService.DetermineViewNameAsync(firstAppointmentId, DateTime.Now.Date, false);

            return View(viewName, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateNotAdmin")]
        public async Task<IActionResult> CreateNotAdmin(CreateTicketDto createDto)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();
            var viewName = await _ticketService.DetermineViewNameAsync(createDto.AppointmentId, createDto.TicketDate, false);

            if (createDto.TicketDate.Date < DateTime.Now.Date)
            {
                var viewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, DateTime.Now.Date, userId, false);
                return View(viewName, viewModel);
            }

            if (!ModelState.IsValid || createDto.SeatId <= 0 || createDto.SeatId > 50 || createDto.CustomerId == null)
            {
                var viewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, false);
                return View(viewName, viewModel);
            }

            var isAvailable = await _ticketService.IsSeatAvailableAsync(createDto.SeatId, createDto.TicketDate, createDto.AppointmentId);
            if (!isAvailable)
            {
                var viewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, false);
                return View(viewName, viewModel);
            }

            await _ticketService.CreateTicketAsync(createDto, userId);

            var isFirstTicket = await _ticketService.IsFirstTicketForCustomerAsync(createDto.CustomerId.Value, createDto.TicketDate, createDto.AppointmentId);
            if (isFirstTicket)
            {
                var customer = await _customerService.GetCustomerByIdAsync(createDto.CustomerId.Value);
                if (customer != null && customer.Phone1 != null)
                    SendWelcomeWhatsApp(customer.Phone1);
            }

            var resultViewModel = await BuildReservationViewModelAsync(createDto.AppointmentId, createDto.TicketDate, userId, false);
            return View(viewName, resultViewModel);
        }

        #endregion

        #region ShowTickets

        public async Task<IActionResult> ShowTicketsForAdmin(Tickets model)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();

            if (model.TicketDate < DateTime.Now.Date && userId != 65)
                model.TicketDate = DateTime.Now.Date;

            var viewName = await _ticketService.DetermineViewNameAsync(model.AppointmentId, model.TicketDate, true);
            var viewModel = await BuildReservationViewModelAsync(model.AppointmentId, model.TicketDate, userId, true);

            return View(viewName, viewModel);
        }

        public async Task<IActionResult> ShowTickets(Tickets model)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetCurrentUserId();
            var isAdmin = IsAdmin();

            if (model.TicketDate.Date < DateTime.Now.Date && !isAdmin)
                model.TicketDate = DateTime.Now.Date;

            var viewName = await _ticketService.DetermineViewNameAsync(model.AppointmentId, model.TicketDate, false);
            var viewModel = await BuildReservationViewModelAsync(model.AppointmentId, model.TicketDate, userId, false);

            return View(viewName, viewModel);
        }

        #endregion

        #region JSON Actions

        public async Task<JsonResult> DeleteTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var userId = GetCurrentUserId();
            var result = await _ticketService.DeleteTicketAsync(id, userId);
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

        public async Task<JsonResult> PayTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.PayTicketAsync(id);
            return Json(result);
        }

        public async Task<JsonResult> CancelPayTicket(int id)
        {
            if (id == 0 || !IsAdmin())
                return Json(false);

            var result = await _ticketService.CancelPayTicketAsync(id);
            return Json(result);
        }

        #endregion

        #region Customer Operations

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

            return Json("لا يوجد");
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomerByCode(string code)
        {
            var customers = await _customerService.SearchCustomersAsync(code);
            var customer = customers.FirstOrDefault();

            if (customer != null)
            {
                var result = $"{customer.CustomerId}&&{customer.FullName}&&{customer.Phone1}&&{customer.Code}&&{customer.Points}";
                return Json(result);
            }

            return Json("لا يوجد");
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomerByPhoneIdForNotAdmin(string Phone)
        {
            var customer = await _customerService.GetCustomerByPhoneAsync(Phone);

            if (customer != null)
            {
                var result = $"{customer.CustomerId}&&{customer.FullName}&&{customer.Phone1}&&{customer.Code}&&{customer.Points}";
                return Json(result);
            }

            return Json("لا يوجد");
        }

        #endregion

        #region ViewModel Builder

        private async Task<TicketViewModel> BuildReservationViewModelAsync(int appointmentId, DateTime date, int userId, bool isAdmin)
        {
            var appointments = await _ticketService.GetUserAppointmentsAsync(userId);
            var customers = await _customerService.GetAllActiveCustomersAsync();
            var suppliers = await _supplierService.GetAllActiveSuppliersAsync();
            var reservedSeats = await _ticketService.GetReservedSeatsAsync(appointmentId, date, userId, isAdmin);

            var viewModel = new TicketViewModel
            {
                AppointmentId = appointmentId,
                TicketDate = date.Date,
                AppointmentsList = new SelectList(appointments, "Id", "Title", appointmentId),
                CustomersList = new SelectList(customers.Where(c => c.FullName != null), "CustomerId", "FullName"),
                SuppliersList = new SelectList(suppliers.Where(s => s.FullName != null), "SupplierId", "FullName"),
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

        #endregion

        #region Helper Methods

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

                var msg = "-نتشرف بخدمتك في شركة النيل للسياحه ونأمل أن تقضي معنا اجمل الرحلات واسعد الاوقات ونرجو التواصل معك قريباً\n\n";
                msg += "-في حالة عدم رغبتك في استقبال اي رسائل مرة اخري برجاء الضغط علي الرابط ادناه\n\n";
                msg += "-ويسعدنا خدمتك دائما وبارك الله فيك\n\n";
                msg += "http://elniltravel.somee.com/Tickets/DontSentAgain?phone=" + number;

                request.AddParameter("token", "xvc5y6q4kzknjmr2");
                request.AddParameter("to", "+2" + number);
                request.AddParameter("body", msg);

                await client.ExecuteAsync(request);
            }
            catch { }
        }

        private async void SendWhatsAppNotifications(string number, int points, string code, int seatNumber, DateTime tDate, string from)
        {
            try
            {
                var url = "https://api.ultramsg.com/instance138410/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, RestSharp.Method.Post);
                request.AddHeader("content-type", "application/json");

                var msg = "*أهلا بك* \n\n*تم تأكيد حجزك* \n\n";
                msg += $"كود : {code} \n";
                msg += $"مقعد : {seatNumber} \n";
                msg += $"نقاط : {points} \n";
                msg += $"يوم : {tDate.ToString("ddd", new CultureInfo("ar-BH"))} - {tDate.ToShortDateString()}\n";
                msg += $"من : {from}\n\n*شكرا لتعاملك معنا*";

                request.AddParameter("token", "xvc5y6q4kzknjmr2");
                request.AddParameter("to", "+2" + number);
                request.AddParameter("body", msg);

                await client.ExecuteAsync(request);
            }
            catch { }
        }

        #endregion
    }
}

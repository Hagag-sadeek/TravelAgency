using System;
using System.Linq;
using TravelAgency.Application.DTOs.Tickets;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<AppointmentBusView> _busViewRepository;
        private readonly IRepository<UserAppointments> _userAppointmentsRepository;

        public TicketService(
            ITicketRepository ticketRepository,
            ICustomerRepository customerRepository,
            ISupplierRepository supplierRepository,
            IAppointmentRepository appointmentRepository,
            IUserRepository userRepository,
            IRepository<AppointmentBusView> busViewRepository,
            IRepository<UserAppointments> userAppointmentsRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _supplierRepository = supplierRepository;
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _busViewRepository = busViewRepository;
            _userAppointmentsRepository = userAppointmentsRepository;
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsWithRelationsAsync()
        {
            var tickets = await _ticketRepository.GetAllWithRelationsAsync();
            return tickets.Select(t => t.ToDto());
        }

        public async Task<TicketDto?> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdWithRelationsAsync(id);
            return ticket?.ToDto();
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto createDto, int userId)
        {
            var ticket = createDto.ToEntity(userId);

            var createdTicket = await _ticketRepository.AddAsync(ticket);

            // Update customer points
            if (ticket.CustomerId.HasValue)
            {
                var customer = await _customerRepository.GetByIdAsync(ticket.CustomerId.Value);
                if (customer != null)
                {
                    customer.Points += 10;
                    await _customerRepository.UpdateAsync(customer);
                }
            }

            await _ticketRepository.SaveChangesAsync();

            var ticketWithRelations = await _ticketRepository.GetByIdWithRelationsAsync(createdTicket.TicketId);
            return ticketWithRelations!.ToDto();
        }

        public async Task<bool> DeleteTicketAsync(int ticketId, int userId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(userId);
            ticket.IsActive = false;
            ticket.Comment = $"Deleted by {user?.Firstname} {DateTime.Now}";

            // Deduct points
            if (ticket.CustomerId.HasValue)
            {
                var customer = await _customerRepository.GetByIdAsync(ticket.CustomerId.Value);
                if (customer != null && customer.Points >= 10)
                {
                    customer.Points -= 10;
                    await _customerRepository.UpdateAsync(customer);
                }
            }

            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsSeatAvailableAsync(int seatNumber, DateTime date, int appointmentId)
        {
            return !await _ticketRepository.IsSeatReservedAsync(seatNumber, date, appointmentId);
        }

        public async Task<bool> IsFirstTicketForCustomerAsync(int customerId, DateTime date, int appointmentId)
        {
            return await _ticketRepository.CustomerHasTicketAsync(customerId, date, appointmentId);
        }

        public async Task<IEnumerable<ReservedSeatDto>> GetReservedSeatsAsync(int appointmentId, DateTime date, int userId, bool isAdmin)
        {
            var tickets = await _ticketRepository.GetByAppointmentAndDateAsync(appointmentId, date);
            var user = await _userRepository.GetByIdAsync(userId);

            return tickets.Select(t => new ReservedSeatDto
            {
                TicketId = t.TicketId,
                CustomerName = t.Customer?.FullName ?? "",
                SupplierName = t.Supplier?.FullName ?? "",
                FromBranch = t.FromBranch?.Title ?? "",
                ToBranch = t.ToBranch?.Title ?? "",
                Phone = t.Customer?.Phone1 ?? "",
                IsFemale = t.IsFemale,
                IsConformed = t.IsConformed,
                IsMine = (t.SupplierId == user?.SupplierId) || isAdmin || (t.UserId == userId),
                SeatId = t.SeatId,
                Price = t.Price,
                Code = t.Customer?.Code ?? ""
            });
        }

        public async Task<bool> ConfirmTicketPriceAsync(int ticketId, int price)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.Price = price;
            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConformTicketAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.IsConformed = true;
            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelConformTicketAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.IsConformed = false;
            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PayTicketAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            // TODO: Add IsPaid property to Tickets entity
            // ticket.IsPaid = true;
            ticket.IsConformed = true; // Using IsConformed as payment confirmation for now
            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelPayTicketAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            // TODO: Add IsPaid property to Tickets entity
            // ticket.IsPaid = false;
            ticket.IsConformed = false;
            await _ticketRepository.UpdateAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return true;
        }

        public async Task<string> DetermineViewNameAsync(int appointmentId, DateTime date, bool isAdmin)
        {
            var baseViewName = isAdmin ? "CreateAdmin" : "CreateNotAdmin";
            var defaultView = baseViewName + "5";

            var allBusViews = await _busViewRepository.GetAllAsync();
            var row = allBusViews
                .Where(x => x.AppointmentId == appointmentId && x.TicketDate.Date == date.Date)
                .OrderByDescending(x => x.AppointmentBusViewtId)
                .FirstOrDefault();

            if (row != null && row.ViewName == "4")
            {
                return baseViewName + "4";
            }

            return defaultView;
        }

        public async Task<object?> GetCustomerInfoByTicketIdAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdWithRelationsAsync(ticketId);
            if (ticket == null || ticket.Customer == null)
            {
                return null;
            }

            var customer = ticket.Customer;
            var info = $"{customer.Phone1}&&{ticket.SupplierId}&&{customer.CustomerId}&&{customer.FullName}&&{customer.Points}";

            return info;
        }

        public async Task<IEnumerable<(int Id, string Title)>> GetUserAppointmentsAsync(int userId)
        {
            var allUserAppointments = await _userAppointmentsRepository.GetAllAsync();
            var currentApps = allUserAppointments
                .Where(x => x.UserId == userId)
                .Select(x => x.AppId)
                .ToList();

            if (currentApps.Count == 0)
            {
                var allAppointments = await _appointmentRepository.GetAllAsync();
                currentApps = allAppointments.Select(x => x.AppointmentId).ToList();
            }

            var appointments = await _appointmentRepository.GetAllActiveAsync();
            return appointments
                .Where(a => currentApps.Contains(a.AppointmentId) && a.Title != null)
                .OrderBy(a => a.SortOrder)
                .Select(a => (a.AppointmentId, a.Title!));
        }
    }
}

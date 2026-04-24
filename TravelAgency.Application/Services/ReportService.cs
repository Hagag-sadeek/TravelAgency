using System;
using System.Linq;
using TravelAgency.Application.DTOs.Reports;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IRepository<Core.Entities.AppointmentPrice> _appointmentPriceRepository;

        public ReportService(
            ITicketRepository ticketRepository,
            ISupplierRepository supplierRepository,
            IAppointmentRepository appointmentRepository,
            IRepository<Core.Entities.AppointmentPrice> appointmentPriceRepository)
        {
            _ticketRepository = ticketRepository;
            _supplierRepository = supplierRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentPriceRepository = appointmentPriceRepository;
        }

        public async Task<TicketReportResultDto> GenerateTicketReportAsync(int appointmentId, DateTime ticketDate)
        {
            var tickets = await _ticketRepository.GetByAppointmentAndDateAsync(appointmentId, ticketDate.Date);
            var supplierDetails = new List<SupplierReportDetailDto>();

            var distinctSupplierIds = tickets.Select(x => x.SupplierId).Distinct().Where(id => id.HasValue).ToList();

            foreach (var supplierId in distinctSupplierIds)
            {
                var supplier = await _supplierRepository.GetByIdAsync(supplierId!.Value);
                if (supplier == null) continue;

                var supplierTickets = tickets.Where(x => x.SupplierId == supplierId).ToList();
                var ticketCount = supplierTickets.Count;

                var allPrices = await _appointmentPriceRepository.GetAllAsync();
                var appointmentPrice = allPrices.FirstOrDefault(x => 
                    x.AppointmentId == appointmentId && 
                    x.SupplierId == supplierId.Value);

                if (appointmentPrice != null)
                {
                    var detail = new SupplierReportDetailDto
                    {
                        SupplierName = supplier.FullName,
                        TicketCount = ticketCount,
                        TotalIncome = ticketCount * appointmentPrice.Price,
                        NetIncome = ticketCount * (appointmentPrice.Price - appointmentPrice.Commision)
                    };

                    supplierDetails.Add(detail);
                }
            }

            return new TicketReportResultDto
            {
                TicketDate = ticketDate,
                AppointmentId = appointmentId,
                SupplierDetails = supplierDetails
            };
        }

        public async Task<IEnumerable<(int Id, string Title)>> GetActiveAppointmentsForReportAsync()
        {
            var appointments = await _appointmentRepository.GetAllActiveAsync();
            return appointments
                .Where(a => a.Title != null)
                .Select(a => (a.AppointmentId, a.Title!));
        }
    }
}

using TravelAgency.Application.DTOs.Tickets;
using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Mappings
{
    public static class TicketMappingExtensions
    {
        public static TicketDto ToDto(this Tickets ticket)
        {
            return new TicketDto
            {
                TicketId = ticket.TicketId,
                CustomerId = ticket.CustomerId,
                CustomerName = ticket.Customer?.FullName,
                SupplierId = ticket.SupplierId,
                SupplierName = ticket.Supplier?.FullName,
                UserId = ticket.UserId,
                AppointmentId = ticket.AppointmentId,
                AppointmentTitle = ticket.Appointment?.Title,
                TicketDate = ticket.TicketDate,
                ReserveDate = ticket.ReserveDate,
                Price = ticket.Price,
                FromBranchId = ticket.FromBranchId,
                FromBranchTitle = ticket.FromBranch?.Title,
                ToBranchId = ticket.ToBranchId,
                ToBranchTitle = ticket.ToBranch?.Title,
                SeatId = ticket.SeatId,
                Comment = ticket.Comment,
                IsActive = ticket.IsActive,
                IsFemale = ticket.IsFemale,
                IsConformed = ticket.IsConformed
            };
        }

        public static Tickets ToEntity(this CreateTicketDto dto, int userId)
        {
            return new Tickets
            {
                CustomerId = dto.CustomerId,
                SupplierId = dto.SupplierId,
                UserId = userId,
                AppointmentId = dto.AppointmentId,
                TicketDate = dto.TicketDate,
                ReserveDate = DateTime.Now,
                Price = dto.Price,
                FromBranchId = dto.FromBranchId,
                ToBranchId = dto.ToBranchId,
                SeatId = dto.SeatId,
                Comment = dto.Comment,
                IsActive = true,
                IsFemale = dto.IsFemale,
                IsConformed = false
            };
        }
    }
}

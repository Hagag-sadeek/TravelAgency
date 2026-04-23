using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class AppointmentsConfiguration : IEntityTypeConfiguration<Appointments>
    {
        public void Configure(EntityTypeBuilder<Appointments> entity)
        {
            entity.HasKey(e => e.AppointmentId);
        }
    }
}

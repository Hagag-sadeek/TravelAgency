using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class UserAppointmentsConfiguration : IEntityTypeConfiguration<UserAppointments>
    {
        public void Configure(EntityTypeBuilder<UserAppointments> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}

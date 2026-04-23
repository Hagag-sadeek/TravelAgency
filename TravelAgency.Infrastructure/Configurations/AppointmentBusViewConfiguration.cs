using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class AppointmentBusViewConfiguration : IEntityTypeConfiguration<AppointmentBusView>
    {
        public void Configure(EntityTypeBuilder<AppointmentBusView> entity)
        {
            entity.HasKey(e => e.AppointmentBusViewtId);
        }
    }
}

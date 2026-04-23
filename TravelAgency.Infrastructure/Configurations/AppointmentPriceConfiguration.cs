using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class AppointmentPriceConfiguration : IEntityTypeConfiguration<AppointmentPrice>
    {
        public void Configure(EntityTypeBuilder<AppointmentPrice> entity)
        {
            entity.HasKey(e => e.AppointmentPriceId);

            entity.HasOne(d => d.Appointment)
                .WithMany(p => p.AppointmentPrice)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_AppointmentPrice_Appointments");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.AppointmentPrice)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_AppointmentPrice_Suppliers");
        }
    }
}

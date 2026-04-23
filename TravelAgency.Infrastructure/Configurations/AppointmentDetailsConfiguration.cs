using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class AppointmentDetailsConfiguration : IEntityTypeConfiguration<AppointmentDetails>
    {
        public void Configure(EntityTypeBuilder<AppointmentDetails> entity)
        {
            entity.HasKey(e => e.AppointmentDetailId);

            entity.HasIndex(e => e.AppointmentId);

            entity.HasIndex(e => e.BranchId);

            entity.HasOne(d => d.Appointment)
                .WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_AppointmentDetails_Appointments");

            entity.HasOne(d => d.Branch)
                .WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentDetails_Branches");
        }
    }
}

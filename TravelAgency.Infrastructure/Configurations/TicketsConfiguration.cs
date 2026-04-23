using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class TicketsConfiguration : IEntityTypeConfiguration<Tickets>
    {
        public void Configure(EntityTypeBuilder<Tickets> entity)
        {
            entity.HasKey(e => e.TicketId);

            entity.HasIndex(e => e.AppointmentId);

            entity.HasIndex(e => e.CustomerId);

            entity.HasIndex(e => e.FromBranchId)
                .HasName("IX_Tickets_FromBraBranchId");

            entity.HasIndex(e => e.SupplierId);

            entity.HasIndex(e => e.ToBranchId)
                .HasDatabaseName("IX_Tickets_ToBraBranchId");

            entity.HasOne(d => d.Appointment)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_Tickets_Appointments");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.FromBranch)
                .WithMany(p => p.TicketsFromBranch)
                .HasForeignKey(d => d.FromBranchId)
                .HasConstraintName("FK_Tickets_Branches");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SupplierId);

            entity.HasOne(d => d.ToBranch)
                .WithMany(p => p.TicketsToBranch)
                .HasForeignKey(d => d.ToBranchId)
                .HasConstraintName("FK_Tickets_Branches1");
        }
    }
}

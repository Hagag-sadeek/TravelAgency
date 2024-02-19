using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelAgency.Models
{
    public partial class TravelAgencyContext : DbContext
    {
        public TravelAgencyContext()
        {
        }

        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<TicketDistributions> TicketDistributions { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<AppointmentPrice> AppointmentPrice { get; set; }  
        public virtual DbSet<AppointmentBusView> AppointmentBusView { get; set; }
        public virtual DbSet<UserAppointments> UserAppointments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TravelAgency;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentDetails>(entity =>
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
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);
            });

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.HasKey(e => e.BranchId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId);
            });

            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.HasIndex(e => e.AppointmentId);

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.FromBranchId)
                    .HasName("IX_Tickets_FromBraBranchId");

                entity.HasIndex(e => e.SupplierId);

                entity.HasIndex(e => e.ToBranchId)
                    .HasName("IX_Tickets_ToBraBranchId");

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
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.BranchId);

                entity.Property(e => e.Firstname).IsRequired();

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Branches");
            });

            modelBuilder.Entity<AppointmentPrice>(entity =>
            {
                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentPrice)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_AppointmentPrice_Appointments");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.AppointmentPrice)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_AppointmentPrice_Suppliers");
            });


            modelBuilder.Entity<AppointmentBusView>(entity =>
            {
                entity.HasKey(e => e.AppointmentBusViewtId);
            });
            modelBuilder.Entity<UserAppointments>(entity =>
            {
                entity.HasKey(e => e.Id);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

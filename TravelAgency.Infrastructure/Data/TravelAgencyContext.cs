using Microsoft.EntityFrameworkCore;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Data
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
        public virtual DbSet<CustomerCancels> CustomerCancels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TravelAgency;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelAgencyContext).Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

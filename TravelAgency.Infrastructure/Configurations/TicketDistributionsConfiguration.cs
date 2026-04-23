using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class TicketDistributionsConfiguration : IEntityTypeConfiguration<TicketDistributions>
    {
        public void Configure(EntityTypeBuilder<TicketDistributions> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}

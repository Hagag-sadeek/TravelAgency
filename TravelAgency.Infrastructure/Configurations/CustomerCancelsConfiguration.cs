using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class CustomerCancelsConfiguration : IEntityTypeConfiguration<CustomerCancels>
    {
        public void Configure(EntityTypeBuilder<CustomerCancels> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class BranchesConfiguration : IEntityTypeConfiguration<Branches>
    {
        public void Configure(EntityTypeBuilder<Branches> entity)
        {
            entity.HasKey(e => e.BranchId);
        }
    }
}

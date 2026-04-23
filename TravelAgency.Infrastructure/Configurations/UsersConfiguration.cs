using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Core.Entities;

namespace TravelAgency.Infrastructure.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
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
        }
    }
}

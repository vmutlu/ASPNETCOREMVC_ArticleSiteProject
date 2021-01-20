using HappyBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class RoleClaimMap : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            // Primary key
            builder.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            builder.ToTable("AspNetRoleClaims");
        }
    }
}

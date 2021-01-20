using HappyBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserClaimMap : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
                builder.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                builder.ToTable("AspNetUserClaims");
        }
    }
}

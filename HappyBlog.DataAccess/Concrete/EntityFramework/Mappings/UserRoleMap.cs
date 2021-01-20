using HappyBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new UserRole
            {
                RoleId = 1,
                UserId = 1
            },
            new UserRole
            {
                UserId = 2,
                RoleId = 2
            });
        }
    }
}

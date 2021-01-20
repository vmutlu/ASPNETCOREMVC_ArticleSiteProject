using HappyBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100);

            builder.Property(c => c.Description).HasMaxLength(500);

            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(70);

            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(70);

            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();

            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();

            builder.Property(c => c.Note).HasMaxLength(500);

            builder.ToTable("Categories");

            builder.HasData(
            new Category
            {
                Id = 1,
                Name = "ASP.NET CORE",
                Description = "ASP.NET CORE Teknolojisi ile ilgili en güncel bilgiler",
                CreatedByName = "InitialCreate",
                ModifiedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Note = "ASP.NET CORE Blog Kategorisi",
                IsActive = true,
                IsDeleted = false
            },
            new Category
            {
                Id = 2,
                Name = "PHP",
                Description = "PHP ile ilgili en güncel bilgiler",
                CreatedByName = "InitialCreate",
                ModifiedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Note = "PHP Blog Kategorisi",
                IsActive = true,
                IsDeleted = false
            },
            new Category
            {
                Id = 3,
                Name = "JS",
                Description = "JS ile ilgili en güncel bilgiler",
                CreatedByName = "InitialCreate",
                ModifiedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Note = "JS Blog Kategorisi",
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}

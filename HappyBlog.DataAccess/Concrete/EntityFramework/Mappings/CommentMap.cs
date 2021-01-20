using HappyBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HappyBlog.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd(); // 1 er 1 er artması için

            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);

            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);

            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);

            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();

            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();

            builder.Property(c => c.Note).HasMaxLength(500);

            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);

            builder.ToTable("Comments");

            //builder.HasData(
            //new Comment
            //{
            //    Id = 1,
            //    ArticleId = 1,
            //    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //    CreatedByName = "InitialCreate",
            //    ModifiedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    Note = "ASP.NET Blog Yazısı",
            //    IsActive = true,
            //    IsDeleted = false
            //},
            //new Comment
            //{
            //    Id = 2,
            //    ArticleId = 2,
            //    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //    CreatedByName = "InitialCreate",
            //    ModifiedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    Note = "PHP Blog Yazısı",
            //    IsActive = true,
            //    IsDeleted = false
            //},
            //new Comment
            //{
            //    Id = 3,
            //    ArticleId = 3,
            //    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //    CreatedByName = "InitialCreate",
            //    ModifiedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    Note = "JS Blog Yazısı",
            //    IsActive = true,
            //    IsDeleted = false
            //});
        }
    }
}

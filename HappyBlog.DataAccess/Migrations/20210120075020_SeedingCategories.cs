using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyBlog.DataAccess.Migrations
{
    public partial class SeedingCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                 "INSERT INTO categories (Name,Description,Note,CreatedDate,CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted) VALUES ('Python','Python Dili ile İlgili En Güncel Bilgiler','Python Kategorisi',CURDATE(),'Migration',CURDATE(),'Migration',1,0)");
            migrationBuilder.Sql(
                            "INSERT INTO categories (Name,Description,Note,CreatedDate,CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted) VALUES ('Java','Java Dili ile İlgili En Güncel Bilgiler','Java Kategorisi',CURDATE(),'Migration',CURDATE(),'Migration',1,0)");
            migrationBuilder.Sql(
                            "INSERT INTO categories (Name,Description,Note,CreatedDate,CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted) VALUES ('Dart','Dart Dili ile İlgili En Güncel Bilgiler','Dart Kategorisi',CURDATE(),'Migration',CURDATE(),'Migration',1,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}

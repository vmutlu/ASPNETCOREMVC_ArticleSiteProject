using HappyBlog.DataAccess.Abstract;
using HappyBlog.DataAccess.Concrete;
using HappyBlog.DataAccess.Concrete.EntityFramework.Contexts;
using HappyBlog.Entities.Concrete;
using HappyBlog.Services.Abstract;
using HappyBlog.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:HappyBlogContext"];
            services.AddDbContext<HappyBlogContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddIdentity<User, Role>(options=>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                /*                                                   Kaynak
                 * https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0
                 */
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<HappyBlogContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}

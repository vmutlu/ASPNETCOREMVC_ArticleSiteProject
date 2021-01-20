using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int ArticlesCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public ArticleListDTO Articles { get; set; }
    }
}

using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDTO CategoryAddDTO { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
    }
}

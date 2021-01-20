using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class CategoryUpdatejaxViewModel
    {
        public CategoryUpdateDTO CategoryUpdateDTO { get; set; }
        public string CategoryUpdatePartial { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
    }
}

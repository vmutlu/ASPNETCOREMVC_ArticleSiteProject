using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDTO UserAddDTO { get; set; }
        public string UserAddPartial { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}

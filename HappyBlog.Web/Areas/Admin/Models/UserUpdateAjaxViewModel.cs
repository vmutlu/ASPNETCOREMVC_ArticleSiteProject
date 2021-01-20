using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDTO UserUpdateDTO { get; set; }
        public string UserUpdatePartial { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}

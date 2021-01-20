using HappyBlog.Entities.Concrete;
using System.Collections.Generic;

namespace HappyBlog.Web.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}

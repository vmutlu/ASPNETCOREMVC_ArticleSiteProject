using HappyBlog.Entities.Concrete;
using HappyBlog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HappyBlog.Web.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        public readonly UserManager<User> _userManager;

        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}

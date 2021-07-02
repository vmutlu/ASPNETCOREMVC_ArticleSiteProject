using Microsoft.AspNetCore.Mvc;

namespace HappyBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

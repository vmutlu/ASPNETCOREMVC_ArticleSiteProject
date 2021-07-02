using HappyBlog.Entities.Concrete;
using HappyBlog.Services.Abstract;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private UserManager<User> _userManager;
        public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _commentService = commentService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var categoriesCount = await _categoryService.CountByNonDeletedAsync().ConfigureAwait(false); ;
            var articlesCount = await _articleService.CountByNonDeletedAsync().ConfigureAwait(false); ;
            var commentsCount = await _commentService.CountByNonDeletedAsync().ConfigureAwait(false); ;
            var usersCount = await _userManager.Users.CountAsync().ConfigureAwait(false); ;
            var articlesResults = await _articleService.GetAllAsync().ConfigureAwait(false); ;

            if(categoriesCount.ResultStatus == ResultStatus.Success && articlesCount.ResultStatus == ResultStatus.Success && commentsCount.ResultStatus == ResultStatus.Success && usersCount > -1 && articlesResults.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCount.Data,
                    ArticlesCount = articlesCount.Data,
                    CommentsCount = commentsCount.Data,
                    UsersCount = usersCount,
                    Articles = articlesResults.Data
                });
            }

            return NotFound();
        }
    }
}

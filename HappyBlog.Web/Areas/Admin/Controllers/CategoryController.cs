using HappyBlog.Entities.DTOs;
using HappyBlog.Services.Abstract;
using HappyBlog.Shared.Utilities.Extensions;
using HappyBlog.Shared.Utilities.Results.ComplexTypes;
using HappyBlog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HappyBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Editor")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDTO categoryAddDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(categoryAddDTO, "Veysel MUTLU");

                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDTO = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDTO)
                    });

                    return Json(categoryAddAjaxModel);
                }
            }

            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDTO)
            });

            return Json(categoryAddAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateAsync(categoryId);
            if (result.ResultStatus == ResultStatus.Success)
                return PartialView("_CategoryUpdatePartial", result.Data);

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(categoryUpdateDTO, "Veysel MUTLU");

                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdatejaxViewModel
                    {
                        CategoryDTO = result.Data,
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDTO)
                    });

                    return Json(categoryUpdateAjaxModel);
                }
            }

            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdatejaxViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDTO)
            });

            return Json(categoryUpdateAjaxErrorModel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId, "Veyseel MUTLU");

            var ajaxResult = JsonSerializer.Serialize(result.Data);
            return Json(ajaxResult);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;
        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCategory()
        {
            var listCategory = await _repo.GetAll();
            return Json(listCategory);
        }
        public async Task<IActionResult> CreateCategory(CategoryDTO cate)
        {
            return RedirectToAction("Index");
        }
    }
}

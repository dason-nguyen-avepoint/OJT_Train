using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly ICategoryRepository _repo;
        public ManagerController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var listCategory = await _repo.GetAll();
            return View();
        }
    }
}

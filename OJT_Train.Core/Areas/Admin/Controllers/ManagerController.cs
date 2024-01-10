using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly IInventoryRepository _repo;
        public ManagerController(IInventoryRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IActionResult> Index()
        {
            var inventories = await _repo.GetInfor();
            return View(inventories);
        }
    }
}

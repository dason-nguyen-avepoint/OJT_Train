using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly IInventoryRepository _repo;
        private readonly IThongKeRepository _thongke;
        public ManagerController(IInventoryRepository repo, IThongKeRepository thongke)
        {
            _repo = repo;
            _thongke = thongke;
        }
        
        public async Task<IActionResult> Index()
        {
            var inventories = await _repo.GetInfor();
            ViewBag.ThongKe = await _thongke.GetInfo();
            return View(inventories);
        }
    }
}

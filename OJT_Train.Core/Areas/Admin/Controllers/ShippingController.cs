using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingController : Controller
    {
        private readonly IOrderRepository _repo;
        public ShippingController(IOrderRepository repo)
        {
            _repo = repo;
        }
        private IActionResult CheckAdminAccess()
        {
            if (HttpContext.Session.GetString("RoleName") == "Admin" || HttpContext.Session.GetString("RoleName") == "Shiper")
            {
                return null;
            }
            return RedirectToAction("Unauthorized", "Error");
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var accessResult = CheckAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }
            int pageSize = 3;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalShips = (int)Math.Ceiling((await _repo.TotalShip()) / (double)pageSize);
            var ships = await _repo.GetShips(pageNumber, pageSize);
            return View(ships);
        }
        [HttpPut]
        public IActionResult ShipCompleted([FromBody] int orderId)
        {
            _repo.ShipCompleted(orderId);
            return Json(new { success = true });
        }
    }
}

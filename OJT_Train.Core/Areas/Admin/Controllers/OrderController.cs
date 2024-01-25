using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repo;
        public OrderController(IOrderRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetOrders()
        {
            var order = await _repo.GetAll();
            return Json(order);
        }
        public async Task<IActionResult> OrderDetails(int Id)
        {
            var orderDetails = await _repo.GetOrderDetail(Id);
            ViewBag.Order = await _repo.GetById(Id);
            return View(orderDetails);
        }
        public IActionResult ShippingOrder(int Id)
        {
            _repo.ShippingOrder(Id);
            return RedirectToAction("Index");
        }
        [HttpPut]
        public IActionResult DeleteOrder([FromBody] int Id)
        {
            _repo.DeleteOrder(Id);
            return Json(new { success = true });
        }
    }
}

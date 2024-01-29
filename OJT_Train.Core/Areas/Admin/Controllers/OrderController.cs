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
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 3;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalOrders = (int)Math.Ceiling((await _repo.TotalOrder()) / (double)pageSize);
            var order = await _repo.GetAll(pageNumber, pageSize);
            return View(order);
        }
        [HttpPut]
        public async Task<IActionResult> OrderDetails([FromBody] int orderId)
        {
            var orderDetail = await _repo.GetOrderDetail(orderId);
            ViewBag.Order = await _repo.GetById(orderId);
            var response = new {success = true, order = ViewBag.Order, orderDetail = orderDetail};
            return Json(response);
        }
        [HttpPut]
        public IActionResult ShippingOrder([FromBody] int Id)
        {
            _repo.ShippingOrder(Id);
            return Json(new { success = true });
        }
        [HttpPut]
        public IActionResult DeleteOrder([FromBody] int Id)
        {
            _repo.DeleteOrder(Id);
            return Json(new { success = true });
        }
    }
}

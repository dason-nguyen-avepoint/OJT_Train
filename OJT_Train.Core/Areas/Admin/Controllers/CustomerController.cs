using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Drawing.Printing;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly IAccountManageRepository _repo;
        private readonly IOrderRepository _order;
        public CustomerController(IAccountManageRepository repo, IOrderRepository order)
        {
            _repo = repo;
            _order = order;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = (int)Math.Ceiling((await _repo.TotalAccount("User", null)) / (double)3);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetData(string? searchBy, int pageNumber = 1)
        {
            int pageSize = 3;
            var users = await _repo.InfoUsers(pageNumber, pageSize, searchBy);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalUsers = (int)Math.Ceiling((await _repo.TotalAccount("User", searchBy)) / (double)pageSize);

            var response = new { success = true, users = users, totalPages = ViewBag.TotalUsers, searchBy = searchBy, currentPage = ViewBag.CurrentPage };
            return Json(response);
        }
        [HttpPut]
        public async Task<IActionResult> UserDetail([FromBody] int userId)
        {
            var user = await _repo.GetById(userId);
            var orders = await _order.GetByUserId(userId);
            var response = new {success=true, user = user, orders = orders};
            return Json(response);
        }
    }
}

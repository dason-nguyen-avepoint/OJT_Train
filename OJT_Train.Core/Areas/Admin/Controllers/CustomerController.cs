using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly IAccountManageRepository _repo;
        public CustomerController(IAccountManageRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? searchBy, int pageNumber = 1)
        {
            int pageSize = 3;
            var users = await _repo.InfoUsers(pageNumber, pageSize, searchBy);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalUsers = (int)Math.Ceiling((await _repo.TotalAccount("User",searchBy)) / (double)pageSize);
            if(searchBy == null)
            {
                return View(users);   
            }
            else
            {
                var response = new { success = true, users = users, totalPages = ViewBag.TotalUsers, searchBy = searchBy, currentPage = ViewBag.CurrentPage };
                return Json(response);
            }
            
        }
    }
}

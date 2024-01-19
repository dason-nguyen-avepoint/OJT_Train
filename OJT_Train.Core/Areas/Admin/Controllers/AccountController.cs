using Microsoft.AspNetCore.Mvc;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountManageRepository _repo;
        public AccountController(IAccountManageRepository repo)
        {
            _repo = repo;
        }
        private async Task<IActionResult> CheckAdminAccess()
        {
            if (HttpContext.Session.GetString("RoleName") != "Admin")
            {
                return RedirectToAction("Unauthorized", "Error");
            }

            return null;
        }
        public async Task<IActionResult> Index()
        {
            //var accessResult = await CheckAdminAccess();
            //if (accessResult != null)
            //{
            //    return accessResult;
            //}
            var accounts = await _repo.GetAll();
            return View(accounts);
        }
        [HttpPut]
        public IActionResult EditAccount([FromBody] AccountManageDTO account)
        {
            _repo.Update(account);
            return Json(new { success = true, data = account });
        }
        [HttpPut]
        public IActionResult DeleteAccount([FromBody] int userId) 
        {
            if(userId == 0 || userId == null)
            {
                return NotFound();
            }
            if(_repo.GetById(userId) == null) 
            {
                return NotFound();
            }
            else
            {
                _repo.Delete(userId);
                return Json(new { success = true });
            }
            
        }
    }
}

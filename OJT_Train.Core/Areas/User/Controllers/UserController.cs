using Mapster;
using Microsoft.AspNetCore.Mvc;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Interfaces;
namespace OJT_Train.Core.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
	{
		private readonly IAccountRepository _accountRepository;

		public UserController(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
        [HttpGet]
        public async Task<IActionResult> UserProfile(int? id)
        {
            var result = await _accountRepository.UspGetProfile(id);
            Account account = new Account();
            account.UserName = result.UserName;
            account.Password = result.Password;
            account.Email = result.Email;
            account.Address = result.Address;
            account.Phone = result.Phone;
            account.DateOfBirth = result.DateOfBirth;
            account.FullName = result.FullName;
            ViewBag.message = TempData["Password"] as string;
            ViewBag.success = TempData["Success"] as string;
            return View(account);
        }
        [HttpPost]
        public IActionResult UpdateProfile(IFormCollection form)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            var fullname = form["txtfullname"];
            var address = form["txtaddress"];
            var phone = form["txtphone"];
            var dateofbith = Convert.ToDateTime(form["txtdateofbirth"]);
            var password = form["txtpassword"];
            var confirmpassword = form["txrepassword"];
            if (password != confirmpassword)
            {
                TempData["Password"] = "Password and Confirm Password are not match";
                return RedirectToAction("UserProfile", new { id = HttpContext.Session.GetInt32("UserID") });
            }
            Account account = new Account();
            account.UserID = userId;
            account.FullName = fullname;
            account.Address = address;
            account.Phone = phone;
            account.DateOfBirth = dateofbith;
            account.Password = password;
            TempData["Success"] = "Update profile sucessful";
            _accountRepository.UpdateProfile(account.Adapt<AccountDTO>());
            ViewBag.Message = "Update Successfully";
            return RedirectToAction("UserProfile", new { id = userId });
        }


    }
}

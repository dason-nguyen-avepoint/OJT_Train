using Mapster;
using Microsoft.AspNetCore.Mvc;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Helpers;
using Repositories.Interfaces;
namespace OJT_Train.Core.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
		private readonly IAccountRepository _accountRepository;

		public HomeController(IAccountRepository accountRepository)
		{

			_accountRepository = accountRepository;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<ActionResult> Login([FromBody]Login login)
		{
			var account = await _accountRepository.getUserforLogin(login.Adapt<LoginDTO>());

			if (account == null)
			{
				return Ok(new { status = false, message = "UserName or Password is not correctly" });
			}
			else
			{
				HttpContext.Session.SetInt32("UserID", account.UserID);
				return Ok(new { status = true, message = "Login successful" });
			}
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(Account model)
		{
			int check = await _accountRepository.UpsRegistration(model.Adapt<AccountDTO>());

			if (check == 1)
			{
				ViewBag.Message = "Username already have, please input another username";
				return View();
			}
			else if (check == 2)
			{
				ViewBag.Message = "Email already have, please input another email";
				return View();
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}

		}
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgotPassword model)
		{

			var result = await _accountRepository.UspForgetPassword(model.Adapt<ForgotPasswordDTO>());
			if (result.UserExists == 0)
			{
				ViewBag.Message = "Email does not exits| Please input correctly email";
				return View();
			}
			else
			{
				EmailHelper.Instance.SendMail(result.Email, result.Password);
				ViewBag.Message = "We already sent new password to your email";
				return View();
			}

		}
		[HttpPost]
		public async Task<IActionResult> Contact(Contact model)
		{
			await _accountRepository.UspContact(model.Adapt<ContactDTO>());
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Home");
		}

	}
}

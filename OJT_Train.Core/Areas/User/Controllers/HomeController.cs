using Mapster;
using Microsoft.AspNetCore.Http;
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
		private readonly IProductRepository _productRepository;
		public HomeController(IAccountRepository accountRepository,IProductRepository productRepository)
		{
			_productRepository= productRepository;
			_accountRepository = accountRepository;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
        {
	
			var check1 = await _productRepository.TopfiveProductbyPriceNew();
			var check2 = await _productRepository.TopfiveProductbyMemories();
			List<Product> listproduct1 = new List<Product>();
			foreach (var item in check1)
			{
				Product product = new Product();
				product.ProductId = item.ProductId;
				product.ProductName = item.ProductName;
				product.Memory = item.Memory;
				product.PriceNew = item.PriceNew;
				product.PriceOld = item.PriceOld;
				product.ProductDetail = item.ProductDetail;
				product.ImageProduct = item.ImageProduct;
				listproduct1.Add(product);
			}
			List<Product> listproduct2 = new List<Product>();
			foreach (var item in check2)
			{
				Product product = new Product();
				product.ProductId = item.ProductId;
				product.ProductName = item.ProductName;
				product.Memory = item.Memory;
				product.PriceNew = item.PriceNew;
				product.PriceOld = item.PriceOld;
				product.ProductDetail = item.ProductDetail;
				product.ImageProduct = item.ImageProduct;
				listproduct2.Add(product);
			}
			var viewModel = new ProductViewModel
			{
				List1 = listproduct1,
				List2 = listproduct2
			};
			return View(viewModel);
        }

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetInt32("User") == null)
			{
				ViewBag.ActivationMessage = TempData["ActivationMessage"] as string;
				return View();
			}
			else
			{
				//HttpContext.SetRoles(new[] { HttpContext.Session.GetString("RoleName") });
				if (HttpContext.Session.GetString("RoleName").Equals("User"))
				{
                    return RedirectToAction("Index", "Home");
                } else if (HttpContext.Session.GetString("RoleName").Equals("Admin"))
				{
                    return RedirectToAction("Index", "Manager", new { area = "Admin" });
				}
				else
				{
                    return RedirectToAction("Index", "Shipping", new { area = "Admin" });
                }
                
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
			else if (account.ISACTIVED == false)
			{	
				return Ok(new { status = false, message = "Please check your email for comfirm account" });
			}
			else
			{
				HttpContext.Session.SetInt32("UserID", account.UserID);
                HttpContext.Session.SetInt32("User", account.UserID);
                HttpContext.Session.SetString("UserName",account.UserName);
				// SET ROLE NAME
				var userId = (int)HttpContext.Session.GetInt32("UserID");
				var roleUser = await _accountRepository.GetRoleUser(userId);
                HttpContext.Session.SetString("RoleName", roleUser);
                
                return Ok(new { status = true, message = "Login successful" });
			}
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(Account model, IFormCollection form)
		{
			 if (model.Password == form["txtPassword"])
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

					int userid = await _accountRepository.Uspgetuseridbyemail(model.Adapt<AccountDTO>());
					EmailHelper.Instance.SendActive(model.Email, userid);
					TempData["ActivationMessage"] = "We already sent email confirm for actived account! Please check your email!";
					return RedirectToAction("Login", "Home");
				}
			}
			else
			{
				ViewBag.Checkpassword = "Password and Confirm password does not match";
				return View();
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
			//HttpContext.Session.Remove("User");
			return RedirectToAction("Login", "Home");
		}
		[HttpGet]
		public async Task<IActionResult> ActiveAccount(int? id)
		{
			await _accountRepository.ActivedAccount(id);
			return RedirectToAction("Login", "Home");
		}

	}
}

using Mapster;
using Microsoft.AspNetCore.Mvc;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Implements;
using Repositories.Interfaces;
using X.PagedList;
namespace OJT_Train.Core.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        private readonly IAccountRepository _accountRepository;
		private readonly IOrderRepository _orderRepository;
		public UserController(IAccountRepository accountRepository, IOrderRepository orderRepository)
		{
			_accountRepository = accountRepository;
			_orderRepository = orderRepository;
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
            Account account = new Account();
            account.UserID = userId;
            account.FullName = fullname;
            account.Address = address;
            account.Phone = phone;
            account.DateOfBirth = dateofbith;
            TempData["Success"] = "Update profile sucessful";
            _accountRepository.UpdateProfile(account.Adapt<AccountDTO>());
            ViewBag.Message = "Update Successfully";
            return RedirectToAction("UserProfile", new { id = userId });
        }

        public async Task<IActionResult> DisplayPurchasedHistory(int? page, int userid)
        {		
            var orderhistory = await _orderRepository.GetOrdercheckhistory(userid);
			var adaptedOrderHistory = orderhistory.Select(order => new OrderForcheck
			{
				OrderId = order.OrderId,
				OrderPrice = order.OrderPrice,
				OrderStatus = order.OrderStatus,
				IsDeleted = order.IsDeleted,
				ProductName = order.ProductName,
				Quantity = order.Quantity
			}).ToList();
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(adaptedOrderHistory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult ChangePassword(int? id)
        {
            ViewBag.Checkpassword = TempData["Checkpassword"] as string;
            ViewBag.success = TempData["message"] as string;
            ViewBag.check = TempData["check"] as string;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword( IFormCollection form)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            var oldpassword = form["txtoldpassword"];
            var newpassword = form["txtpassword"];
            var repassword = form["txtrepassword"];
            Account account = new Account();
            account.Password = oldpassword;
            account.UserID= userId;

            if(newpassword!= repassword)
            {
                TempData["Checkpassword"]= "Repassword and Comfirm password does not match";

                RedirectToAction("ChangePassword", new { id = userId });
            }
            else
            {
                int check = await _accountRepository.ChangePassword(account.Adapt<AccountDTO>(), newpassword);
                if (check == 1)
                {
                    TempData["message"] = "Update password successful";
                    RedirectToAction("ChangePassword", new { id = userId });
                }
                else
                {
                    TempData["check"] = "Old password does not correctly";
                }
            }
            return RedirectToAction("ChangePassword", new { id = userId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderID)
        {
            await _orderRepository.UpdateOrder(orderID);
            return Ok(new { status = 200 });
        }
    }

}

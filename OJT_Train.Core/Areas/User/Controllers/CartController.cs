using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Interfaces;
namespace OJT_Train.Core.Areas.User.Controllers
{
	[Area("User")]
	public class CartController : Controller
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IProductRepository _productRepository;
		private readonly IPromotionuRepository _promotionuRepository;
		private readonly IOrderRepository _orderRepository;
		public CartController(IProductRepository productRepository, IPromotionuRepository promotionuRepository, IAccountRepository accountRepository, IOrderRepository orderRepository)
		{
			_productRepository = productRepository;
			_promotionuRepository = promotionuRepository;
			_accountRepository = accountRepository;
			_orderRepository = orderRepository;
		}
		public IActionResult ViewCart()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ViewPaymentForm()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> CheckOut(string? coupon)
		{
			int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            var list = ListItemCart();
            var listOfUser = list.Where(x => x.UserID == userId).ToList();
			if(listOfUser.Count() == 0)
			{
				return Ok(new { status = 202 });
			}
            var couponDetail = await _promotionuRepository.UspGetPromotionu(coupon);
			var userDTO = await _accountRepository.UspGetProfile(userId);
			var userInfo = new Account();
			userInfo.UserID = userDTO.UserID;
			userInfo.UserName = userDTO.UserName;
			userInfo.FullName = userDTO.FullName;
			userInfo.Email = userDTO.Email;
			userInfo.Address = userDTO.Address;	
			userInfo.Phone = userDTO.Phone;
			
			decimal totalCart = 0;
			foreach (var item in listOfUser)
			{
				totalCart += Convert.ToDecimal(item.ItemPriceTotal);
			}
			if(couponDetail != null)
			{
                double discountPercentage = Convert.ToDouble(couponDetail.Promotionvalue) / 100;
                decimal discountAmount = totalCart * Convert.ToDecimal(discountPercentage);
                totalCart -= discountAmount;
            }
			InforCheckOut inforCheckOut = new InforCheckOut();
			inforCheckOut.Account = userInfo;
			inforCheckOut.TotalPrice = totalCart;
			inforCheckOut.CartItems = listOfUser;
			HttpContext.Session.SetObjectAsJson("InforCheckOut", inforCheckOut);
			return Ok(new { status = 200});
		}


		[HttpGet]
		public IActionResult  Orderdetail()
		{
            InforCheckOut inforCheckOut = HttpContext.Session.GetObjectFromJson<InforCheckOut>("InforCheckOut");
			_orderRepository.AddOrderandOrderDetail(inforCheckOut.TotalPrice, inforCheckOut.Account.Email, inforCheckOut.Account.Address, inforCheckOut.Account.UserID, inforCheckOut.CartItems.Adapt<List<OrderU>>());
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            List<CartItem> lstCart = ListItemCart();
            var listWithoutUserCheckOut = lstCart.Where(x => x.UserID != userId).ToList();
            HttpContext.Session.SetObjectAsJson("ItemCart", listWithoutUserCheckOut);
            HttpContext.Session.Remove("InforCheckOut");
            return Ok(new { status = 200 });
		}



		public List<CartItem> ListItemCart()
		{
			List<CartItem> cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("ItemCart");

			if (cartItems == null)
			{
				cartItems = new List<CartItem>();
				HttpContext.Session.SetObjectAsJson("ItemCart", cartItems);
			}
			return cartItems;
		}

		public async Task<ActionResult> AddToCart(int productID)
		{
            try
			{
				int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
				var productdetail = await _productRepository.GetProductByID(productID);
				List<CartItem> lstCart = ListItemCart();

				CartItem spCheck = lstCart.SingleOrDefault(x => x.ProductID == productID);
				if (spCheck != null)
				{
					spCheck.Quantity++;
					spCheck.ItemPriceTotal = spCheck.Quantity * spCheck.Price;
					HttpContext.Session.SetObjectAsJson("ItemCart", lstCart);
					return Ok(new { sttus = 200, lstCart });
				}
				var currentPrice = productdetail.PriceNew == 0 ? productdetail.PriceOld : productdetail.PriceNew;
				CartItem item = new CartItem(productID, productdetail.ProductName, productdetail.ImageProduct, currentPrice, 1, userId);
				lstCart.Add(item);
				HttpContext.Session.SetObjectAsJson("ItemCart", lstCart);
				return Ok(new { status = 200, lstCart = lstCart });
			}
			catch
			{
				return Ok(new { status = 400 });
			}

		}

		public async Task<ActionResult> GetQuantityInCart()
		{
			try
			{
                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                var list = ListItemCart();
				var listCartOfUser = list.Where(x => x.UserID == userId);
				return Ok(new { status = 200, countItem = listCartOfUser.Count() });
			}
			catch
			{
				return Ok(new { status = 400 });
			}
			
		}

		public async Task<ActionResult> OnClickToViewCart()
		{
			try
			{
                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
                var list = ListItemCart();
                var listCartOfUser = list.Where(x => x.UserID == userId).ToList();
                decimal totalCart = 0;
				foreach (var item in listCartOfUser)
				{
					totalCart += Convert.ToDecimal(item.ItemPriceTotal);
				}	
				return Ok(new { status = 200, listCart = listCartOfUser, totalCart = totalCart, countItem = listCartOfUser.Count() });
			}
			catch
			{
				return Ok(new { status = 400 });
			}
		}
		[HttpPost]
		public ActionResult DeleteProduct(int productId)
		{
			try
			{
				int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
				var list = ListItemCart();
				var listCartOfUser = list.Where(x => x.UserID == userId).ToList();
				CartItem deleteProduct = listCartOfUser.SingleOrDefault(x => x.ProductID == productId);
				listCartOfUser.Remove(deleteProduct);
				//var removedItem = listCartOfUser.RemoveAll(x => x.ProductID == productId);
				decimal totalCart = 0;
                if (listCartOfUser.Count > 0)
				{
                    totalCart = listCartOfUser.Sum(item => Convert.ToDecimal(item.ItemPriceTotal));
					
				}
                HttpContext.Session.SetObjectAsJson("ItemCart", listCartOfUser);
                return Ok(new { status = 200, listCart = listCartOfUser, totalCart = totalCart, countItem = listCartOfUser.Count() });
            }
			catch (Exception ex)
			{
				// Log the exception details for debugging purposes.
				Console.WriteLine($"Exception: {ex.Message}");
				return Ok(new { status = 500, message = "Internal Server Error" });
			}
		}

		[HttpPost]
		public async Task<ActionResult> CheckCoupon([FromBody] string coupon)
		{
			try
			{
				var getCoupon = await _promotionuRepository.UspGetPromotionu(coupon);
				if(getCoupon == null)
				{
					return Ok(new { status = 400});
				} else
				{
					return Ok(new { status = 200, getCoupon = getCoupon });
				}
			}
			catch
			{
				return Ok(new { status = 400 });
			}
		}
		
	}

	public static class SessionExtensions
	{
		public static void SetObjectAsJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetObjectFromJson<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}


	}

	
}

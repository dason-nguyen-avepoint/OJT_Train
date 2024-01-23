using Microsoft.AspNetCore.Mvc;

namespace OJT_Train.Core.Areas.User.Controllers
{
	[Area("User")]
	public class CartController : Controller
	{
		
		public IActionResult ViewCart()
		{
			return View();
		}
	}
}

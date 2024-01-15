using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> ProfileUser(int? id)
		{
			var result = await _accountRepository.UspGetProfile(id);
			return View();
		}
		
	}
}

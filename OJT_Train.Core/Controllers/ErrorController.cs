using Microsoft.AspNetCore.Mvc;

namespace OJT_Train.Core.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Unauthorized")]
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}

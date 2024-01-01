using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}

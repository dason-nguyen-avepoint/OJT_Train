using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly ISaleRepository _repo;
        public SaleController(ISaleRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetSales()
        {
            var sales = await _repo.GetAll();
            return Json(sales);
        }
        [HttpPost]
        public IActionResult DeleteSale([FromBody] SaleDTO model)
        {
            _repo.Delete(model);
            return Json(new { success = true, data = model });
        }
    }
}

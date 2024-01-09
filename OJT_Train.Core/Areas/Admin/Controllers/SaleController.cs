using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly ISaleRepository _repo;
        private readonly IProductRepository _product;
        public SaleController(ISaleRepository repo, IProductRepository product)
        {
            _repo = repo;
            _product = product;
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
        public async Task<IActionResult> Create()
        {
            ViewBag.ListProducts = await _product.GetValidProduct();
            return View();
        }
        [HttpPost]
        public IActionResult CreateSale(SaleDTO sale)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(sale);
                return Json(new { redirectUrl = Url.Action("Index", "Sale", new { area = "Admin" }) });
            }
            // If ModelState is not valid, return a validation error response
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return BadRequest("Sale ID is not provided");
            }
            try
            {
                var sale = await _repo.GetById(Id);
                if (sale == null)
                {
                    return NotFound($"Sale with ID {Id} not found");
                }
                ViewBag.ListProducts = await _product.GetValidProduct();
                return View(sale);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut]
        public IActionResult UpdateSale(UpdateSaleDTO sale)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(sale);
                return Json(new { redirectUrl = Url.Action("Index", "Sale", new { area = "Admin" }) });
            }
            return BadRequest(ModelState);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetAll();
            return Json(products);
        }
        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductDTO product)
        {
            _repo.Delete(product);
            return Json(new { success = true, data = product });
        }
    }
}

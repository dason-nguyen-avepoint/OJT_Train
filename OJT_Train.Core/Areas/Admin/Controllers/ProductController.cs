using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _cate;
        public ProductController(IProductRepository repo, ICategoryRepository cate)
        {
            _repo = repo;
            _cate = cate;
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
        public async Task<IActionResult> Create() 
        {
            var categories = await _cate.GetAll();
            ViewBag.ListCate = categories;
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDTO product)
        {
            return Ok(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductDTO product)
        {
            _repo.Delete(product);
            return Json(new { success = true, data = product });
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dto;
using Repositories.Interfaces;

namespace OJT_Train.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository repo, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult CreateProduct([FromBody] ProductDTO productDTO)
        {
            string uniqueFileName = UploadedFile(productDTO);
        }
        private string UploadedFile(ProductDTO model)
        {
            string uniqueFileName = null;

            if (model.ImageProduct != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductDTO product)
        {
            _repo.Delete(product);
            return Json(new { success = true, data = product });
        }
    }
}

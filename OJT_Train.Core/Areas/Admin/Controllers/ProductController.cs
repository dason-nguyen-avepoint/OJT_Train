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
        private readonly ICategoryRepository _cate;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository repo, ICategoryRepository cate, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _cate = cate;
            _webHostEnvironment = webHostEnvironment;
        }
        private IActionResult CheckAdminAccess()
        {
            if (HttpContext.Session.GetString("RoleName") != "Admin")
            {
                return RedirectToAction("Unauthorized", "Error");
            }

            return null;
        }
        public IActionResult Index()
        {
            var accessResult = CheckAdminAccess();
            if (accessResult != null)
            {
                return accessResult;
            }
            return View();
        }
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetAll();
            return Json(products);
        }
        public async Task<IActionResult> Create() 
        {
            ViewBag.ListCate = await _cate.GetPublishedCate();
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductDTO product, IFormFile imageProductFile)
        {
            if(ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                try
                {
                    
                    if(imageProductFile != null && imageProductFile.Length > 0)
                    {
                        string productPath = Path.Combine(wwwRoot, "Images", "Products");
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageProductFile.FileName);
                        using (var stream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            imageProductFile.CopyTo(stream);
                        }
                        product.ImageProduct = "/Images/Products/" + fileName;
                    }
                    // Add the productDTO to the database or your data storage
                    _repo.Add(product);
                    return Json(new{ redirectUrl = Url.Action("Index", "Product", new { area = "Admin" }) });
                }
                catch (Exception ex)
                {
                    // Handle exception and return an error response
                    return BadRequest($"Error creating product: {ex.Message}");
                }
            }
            // If ModelState is not valid, return a validation error response
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductDTO product)
        {
            _repo.Delete(product);
            return Json(new { success = true, data = product });
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return BadRequest("Product ID is not provided");
            }
            try
            {
                var product = await _repo.GetById(Id);
                if (product == null)
                {
                    return NotFound($"Product with ID {Id} not found");
                }
                ViewBag.ListCate = await _cate.GetPublishedCate();
                return View(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        public IActionResult UpdateProduct(ProductDTO product, IFormFile? imageProductFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (imageProductFile != null && imageProductFile.Length > 0)
                {
                    string productPath = Path.Combine(wwwRoot, "Images", "Products");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageProductFile.FileName);
                    //Delete old image
                    var oldImageUrl = Path.Combine(wwwRoot, "Images", "Products", Path.GetFileName(product.ImageProduct));
                    if (System.IO.File.Exists(oldImageUrl))
                    {
                        System.IO.File.Delete(oldImageUrl);
                    }
                    using (var stream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        imageProductFile.CopyTo(stream);
                    }
                    product.ImageProduct = "/Images/Products/" + fileName;
                }
                _repo.Update(product);
                return Json(new { redirectUrl = Url.Action("Index", "Product", new { area = "Admin" }) });
            }
            return BadRequest(ModelState);
        }
        //public async Task<IActionResult> ImageProduct(int productId)
        //{
        //    if (productId == null || productId == 0)
        //    {
        //        return BadRequest("Product ID is not provided");
        //    }
        //    try
        //    {
        //        var product = await _repo.GetById(productId);
        //        if (product == null)
        //        {
        //            return NotFound($"Product with ID {productId} not found");
        //        }
        //        return View(product);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}
    }
}

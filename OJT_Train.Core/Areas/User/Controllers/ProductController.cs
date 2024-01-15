using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol;
using OJT_Train.Core.Areas.User.Models;
using Repositories.Dto;
using Repositories.Interfaces;
using System.Web;
using X.PagedList;
namespace OJT_Train.Core.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository)
        {

            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> ViewStore(int? page)
        {
            var listcategory = await _categoryRepository.GetAllCategory();
            List<Category> resultCate = new List<Category>();
            foreach (var item1 in listcategory)
            {
                var category = new Category();
                category.CategoryId = item1.CategoryId;
                category.CategoryName = item1.CategoryName;
                resultCate.Add(category);
            }
            ViewBag.ListCate = resultCate;
            var listProduct = await _productRepository.GetAllProduct();
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> resultProduct = new List<Product>();
            foreach (var item in listProduct)
            {
                Product product = new Product();
                product.ProductId = item.ProductId;
                product.ProductName = item.ProductName;
                product.CategoryId = item.CategoryId;
                product.Memory = item.Memory;
                product.PriceNew = item.PriceNew;
                product.PriceOld = item.PriceOld;
                product.ProductDetail = item.ProductDetail;
                product.ImageProduct = item.ImageProduct;
                resultProduct.Add(product);
            }
            return View(resultProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductByCategory(int? id, int? page)
        {
            var listcategory = await _categoryRepository.GetAllCategory();
            List<Category> resultCate = new List<Category>();
            foreach (var item1 in listcategory)
            {
                var category = new Category();
                category.CategoryId = item1.CategoryId;
                category.CategoryName = item1.CategoryName;
                resultCate.Add(category);
            }
            ViewBag.ListCate = resultCate;
            var listproduct = await _productRepository.GetProductByCategory(id);
            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            List<Product> resultProductbycategory = new List<Product>();
            foreach (var item in listproduct)
            {
                Product product = new Product();
                product.ProductId = item.ProductId;
                product.ProductName = item.ProductName;
                product.CategoryId = item.CategoryId;
                product.Memory = item.Memory;
                product.PriceNew = item.PriceNew;
                product.PriceOld = item.PriceOld;
                product.ProductDetail = item.ProductDetail;
                product.ImageProduct = item.ImageProduct;
                resultProductbycategory.Add(product);
            }
            return View(resultProductbycategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> ViewProductDetail(int id)
        {
            var productdetail = await _productRepository.GetProductByID(id);
            var returnProduct = new Product();
            returnProduct.ProductId = productdetail.ProductId;
            returnProduct.ProductName = productdetail.ProductName;
            returnProduct.Memory = productdetail.Memory;
            returnProduct.PriceOld = productdetail.PriceOld;
            returnProduct.PriceNew = productdetail.PriceNew;
            returnProduct.ProductDetail = HttpUtility.HtmlDecode(productdetail.ProductDetail);
            returnProduct.ImageProduct = productdetail.ImageProduct;
            var listComment = await _commentRepository.GetAllComment(id);
            List<Comment> comments = new List<Comment>();
            foreach (var commentitem in listComment)
            {
                var comment = new Comment();
                comment.CommentID = commentitem.CommentID;
                comment.CommentContent = commentitem.CommentContent;
                comment.CreatedDate = commentitem.CreatedDate;
                comment.UserName = commentitem.UserName;
                comments.Add(comment);
            }
            ViewBag.CommentList = comments;
            return View(returnProduct);
        }
    }
}

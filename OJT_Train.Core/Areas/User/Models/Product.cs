using Repositories.Dto;

namespace OJT_Train.Core.Areas.User.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string? Memory { get; set; }
        public int PriceOld { get; set; }
        public int PriceNew { get; set; }
        public string? ProductDetail { get; set; }
        public string? ImageProduct { get; set; }

        public List<Comment> Comments { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
    public class ProductDetailImg
    {
        public int ImgID { get; set; }
        public string? ImgProduct { get; set; }
    }
}

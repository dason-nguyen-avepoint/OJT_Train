using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dto
{
    public class ProductInUserViewDTO
    {
        public int ProductId { get; set; }  
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string? Memory { get; set; }
        public int PriceNew { get; set; }
        public int PriceOld { get; set; }
        public string? ProductDetail { get; set; }
        public string? ImageProduct { get; set; }
    }

    public class CategoryProductDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class ProductDetailImgDto
    {
        public int ImgID { get; set;}
        public string? ImgProduct { get; set; }
    }
}

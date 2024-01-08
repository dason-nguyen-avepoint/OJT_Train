namespace Repositories.Dto
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ? ProductName { get; set; }
        public string ? Memory { get; set; }
        public int PriceOld { get; set; }
        public int PriceNew { get; set; }
        public string ? ProductDetail { get; set; }
        public string ? ImageProduct { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public string ? CreatedBy { get; set; }
        public string ? ModifiedBy { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public string ? CategoryName { get; set; }
    }
}

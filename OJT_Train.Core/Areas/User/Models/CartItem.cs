namespace OJT_Train.Core.Areas.User.Models
{
    public class CartItem
    {
        public CartItem()
        {

        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public Nullable<long> Price { get; set; }
        public Nullable<int> Quantity { get; set; }

        public int UserID { get; set; }
        public Nullable<decimal> ItemPriceTotal { get; set; }

        public CartItem(int productID, string productName, string productImg, long price, int quantity, int userId)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.ProductImg = productImg;
            this.Price = price;
            this.Quantity = quantity;
            this.UserID = userId;
            this.ItemPriceTotal = price * quantity;

		}
    }
	public class OrderForcheck
	{
		public int OrderId { get; set; }
		public long OrderPrice { get; set; }

		public string? OrderStatus { get; set; }
		public bool IsDeleted { get; set; }

		public string? ProductName { get; set; }

		public int Quantity { get; set; }

	}

	public class InforCheckOut
    {
        public InforCheckOut()
        {

        }

        public Account Account { get; set; }
        public List<CartItem> CartItems { get; set; }   
        public decimal TotalPrice { get; set; }
    } 


}

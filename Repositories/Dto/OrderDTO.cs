namespace Repositories.Dto
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public long OrderPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Address { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? OrderStatus { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
    }
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ImageProduct { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public int OrderId { get; set; }
    }
    public class OrderById
    {
        public int OrderId { get; set; }
        public long OrderPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Address { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? OrderStatus { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public string? UserAddress { get; set; }
        public string? Phone { get; set; }
    }
    public class OrderU
    {
		public int ProductID { get; set; }
		public Nullable<long> Price { get; set; }
		public Nullable<int> Quantity { get; set; }

		public int UserID { get; set; }
	}
    public class OrderForcheckHistory
    {
        public int OrderId { get; set; }
		public long OrderPrice { get; set; }

		public string? OrderStatus { get; set; }
		public bool IsDeleted { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

	}
}
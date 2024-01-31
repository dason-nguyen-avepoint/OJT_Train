using Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAll(int pageNumber, int pageSize);
        Task<OrderById> GetById(int id);
        Task<IEnumerable<OrderById>> GetByUserId(int userId);
        void ShippingOrder(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetail(int id);
        //Task<int> UspOrder(int orderPrice, int userID, string createBy, string address);
        void AddOrderandOrderDetail(decimal OrderPrice, string CreatedBy, string Address, int UserID, List<OrderU> model);
		void DeleteOrder(int id);
		Task<int> TotalOrder();
    }
}
